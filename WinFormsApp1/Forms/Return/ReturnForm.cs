using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Return;

public partial class ReturnForm : Form
{
    private readonly ReturnService _returnService;
    private readonly AppDbContext _context;
    private Member? _selectedMember;
    private List<BorrowRecord> _currentRecords = new();

    public ReturnForm(ReturnService returnService, AppDbContext context)
    {
        _returnService = returnService;
        _context = context;
        InitializeComponent();
        Load += ReturnForm_Load;
    }

    private async void ReturnForm_Load(object? sender, EventArgs e)
    {
        cmbCondition.SelectedIndex = 0;
        await LoadMembersAsync();
    }

    private List<Member> _allMembers = new();
    private bool _isFiltering = false;

    private async Task LoadMembersAsync()
    {
        _allMembers = await _context.Members
            .Where(m => m.Status == Models.Enums.MemberStatus.Active)
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .ToListAsync();

        RefreshMemberDropdown("");
    }

    private void RefreshMemberDropdown(string filter)
    {
        _isFiltering = true;
        var text = cmbMembers.Text;
        var selectedId = _selectedMember?.Id;

        var filtered = string.IsNullOrWhiteSpace(filter)
            ? _allMembers
            : _allMembers.Where(m =>
                m.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                m.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                m.Email.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

        cmbMembers.DataSource = filtered.Select(m => new
        {
            m.Id,
            Display = $"{m.FirstName} {m.LastName} - {m.Email}"
        }).ToList();
        cmbMembers.DisplayMember = "Display";
        cmbMembers.ValueMember = "Id";
        cmbMembers.Text = text;

        if (selectedId.HasValue)
        {
            var idx = cmbMembers.Items.Cast<dynamic>().ToList()
                .FindIndex(x => (int)x.Id == selectedId.Value);
            if (idx >= 0) cmbMembers.SelectedIndex = idx;
        }

        _isFiltering = false;
    }

    private void cmbMembers_TextUpdate(object? sender, EventArgs e)
    {
        if (_isFiltering) return;
        RefreshMemberDropdown(cmbMembers.Text);
    }

    private async void cmbMembers_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbMembers.SelectedIndex < 0)
        {
            ClearMemberInfo();
            return;
        }

        var selected = cmbMembers.SelectedItem;
        var memberId = (int)selected.GetType().GetProperty("Id")!.GetValue(selected)!;

        _selectedMember = await _returnService.SearchMemberAsync(
            _context.Members.Find(memberId)?.Email ?? "");

        if (_selectedMember == null)
        {
            ClearMemberInfo();
            return;
        }

        var card = _selectedMember.LibraryCard;
        lblMemberName.Text = $"Name: {_selectedMember.FirstName} {_selectedMember.LastName}";
        lblMemberEmail.Text = $"Email: {_selectedMember.Email}";
        lblMemberCard.Text = card != null
            ? $"Card: {card.CardNumber} (Expires: {card.ExpiryDate:yyyy-MM-dd})"
            : "Card: Not issued";

        await LoadMemberBorrowsAsync();
    }

    private async Task LoadMemberBorrowsAsync()
    {
        if (_selectedMember == null)
        {
            dgvBorrows.DataSource = null;
            _currentRecords.Clear();
            btnReturn.Enabled = false;
            btnReturnMultiple.Enabled = false;
            return;
        }

        _currentRecords = await _returnService.GetActiveBorrowsAsync(_selectedMember.Id);
        BindGrid();

        var count = _currentRecords.Count;
        lblMemberBorrows.Text = $"Active Borrows: {count}";
    }

    private async void btnLoadOverdue_Click(object? sender, EventArgs e)
    {
        lblError.Text = "";
        cmbMembers.SelectedIndex = -1;
        _selectedMember = null;
        ClearMemberInfo();

        _currentRecords = await _returnService.GetOverdueAsync();
        BindGrid();

        lblMemberBorrows.Text = $"Overdue Books: {_currentRecords.Count}";
    }

    private void BindGrid()
    {
        dgvBorrows.DataSource = _currentRecords.Select(br => new
        {
            br.Id,
            Member = $"{br.Member.FirstName} {br.Member.LastName}",
            BookTitle = br.BookCopy.Book.Title,
            br.BookCopy.Barcode,
            BorrowDate = br.BorrowDate.ToLocalTime().ToString("yyyy-MM-dd"),
            DueDate = br.DueDate.ToLocalTime().ToString("yyyy-MM-dd"),
            DaysOverdue = br.DueDate < DateTime.UtcNow
                ? (DateTime.UtcNow - br.DueDate).Days
                : 0
        }).ToList();

        btnReturn.Enabled = _currentRecords.Count > 0;
        btnReturnMultiple.Enabled = _currentRecords.Count > 0;
        UpdateFeeInfo();
    }

    private void dgvBorrows_SelectionChanged(object? sender, EventArgs e)
    {
        UpdateFeeInfo();
    }

    private void UpdateFeeInfo()
    {
        if (dgvBorrows.SelectedRows.Count == 0)
        {
            lblFeeInfo.Text = "";
            return;
        }

        decimal totalFee = 0;
        int overdueCount = 0;

        foreach (DataGridViewRow row in dgvBorrows.SelectedRows)
        {
            if (row.Index >= _currentRecords.Count) continue;
            var record = _currentRecords[row.Index];
            if (record.DueDate < DateTime.UtcNow)
            {
                var days = (DateTime.UtcNow - record.DueDate).Days;
                totalFee += days * 5000m;
                overdueCount++;
            }
        }

        if (overdueCount > 0)
            lblFeeInfo.Text = $"Selected: {dgvBorrows.SelectedRows.Count} book(s) | Overdue: {overdueCount} | Estimated fee: {totalFee:N0} VND";
        else
            lblFeeInfo.Text = $"Selected: {dgvBorrows.SelectedRows.Count} book(s) | No late fees";
    }

    private async void btnReturn_Click(object? sender, EventArgs e)
    {
        if (dgvBorrows.CurrentRow == null || dgvBorrows.CurrentRow.Index < 0)
        {
            lblError.Text = "Please select a borrow record.";
            return;
        }

        var rowIndex = dgvBorrows.CurrentRow.Index;
        if (rowIndex >= _currentRecords.Count) return;

        var record = _currentRecords[rowIndex];
        var condition = GetConditionCode();

        var confirm = MessageBox.Show(
            $"Return '{record.BookCopy.Book.Title}'?\nCondition: {cmbCondition.Text}",
            "Confirm Return",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        var result = await _returnService.ReturnAsync(record.Id, SessionManager.GetCurrentUserId(), condition);

        if (result.Success)
        {
            MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _currentRecords.RemoveAt(rowIndex);
            BindGrid();
            if (_selectedMember != null)
                lblMemberBorrows.Text = $"Active Borrows: {_currentRecords.Count}";
        }
        else
        {
            lblError.Text = result.Message;
        }
    }

    private async void btnReturnMultiple_Click(object? sender, EventArgs e)
    {
        if (dgvBorrows.SelectedRows.Count == 0)
        {
            lblError.Text = "Please select one or more rows.";
            return;
        }

        var ids = new List<int>();
        foreach (DataGridViewRow row in dgvBorrows.SelectedRows)
        {
            if (row.Index < _currentRecords.Count)
                ids.Add(_currentRecords[row.Index].Id);
        }

        if (ids.Count == 0) return;

        var condition = GetConditionCode();

        var confirm = MessageBox.Show(
            $"Return {ids.Count} book(s)?\nCondition: {cmbCondition.Text}",
            "Confirm Return Multiple",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        var result = await _returnService.ReturnMultipleAsync(ids, SessionManager.GetCurrentUserId(), condition);

        MessageBox.Show(result.Message, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

        _currentRecords.RemoveAll(r => ids.Contains(r.Id));
        BindGrid();
        if (_selectedMember != null)
            lblMemberBorrows.Text = $"Active Borrows: {_currentRecords.Count}";
    }

    private string GetConditionCode()
    {
        return cmbCondition.SelectedIndex switch
        {
            1 => "damaged",
            2 => "lost",
            _ => "good"
        };
    }

    private void ClearMemberInfo()
    {
        _selectedMember = null;
        lblMemberName.Text = "Name: -";
        lblMemberEmail.Text = "Email: -";
        lblMemberCard.Text = "Card: -";
        lblMemberBorrows.Text = "Active Borrows: -";
        dgvBorrows.DataSource = null;
        _currentRecords.Clear();
        btnReturn.Enabled = false;
        btnReturnMultiple.Enabled = false;
        lblFeeInfo.Text = "";
    }

    private void btnBack_Click(object? sender, EventArgs e)
    {
        this.Close();
    }
}
