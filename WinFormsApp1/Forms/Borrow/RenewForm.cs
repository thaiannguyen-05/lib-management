using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Borrow;

public partial class RenewForm : Form
{
    private readonly BorrowService _borrowService;
    private readonly RenewalService _renewalService;
    private readonly AppDbContext _context;

    private Member? _selectedMember;
    private List<BorrowRecord> _currentBorrows = new();
    private BorrowRecord? _selectedBorrowRecord;
    private int? _initialMemberId;
    private int? _initialBorrowRecordId;

    public RenewForm(BorrowService borrowService, RenewalService renewalService, AppDbContext context)
    {
        _borrowService = borrowService;
        _renewalService = renewalService;
        _context = context;
        InitializeComponent();
        Load += RenewForm_Load;
    }

    public void InitializeSelection(int? memberId, int? borrowRecordId)
    {
        _initialMemberId = memberId;
        _initialBorrowRecordId = borrowRecordId;
    }

    private async void RenewForm_Load(object? sender, EventArgs e)
    {
        await LoadMembersAsync();
    }

    private async Task LoadMembersAsync()
    {
        var members = await _context.Members
            .Where(m => m.Status == MemberStatus.Active)
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .Select(m => new
            {
                m.Id,
                Display = $"{m.FirstName} {m.LastName} - {m.Email}"
            })
            .ToListAsync();

        cmbMembers.DataSource = members;
        cmbMembers.DisplayMember = "Display";
        cmbMembers.ValueMember = "Id";
        cmbMembers.SelectedIndex = -1;

        if (_initialMemberId.HasValue)
        {
            var index = members.FindIndex(x => x.Id == _initialMemberId.Value);
            if (index >= 0)
            {
                cmbMembers.SelectedIndex = index;
            }
        }
    }

    private async void cmbMembers_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbMembers.SelectedIndex < 0 || cmbMembers.SelectedItem == null)
        {
            ClearMemberInfo();
            return;
        }

        var memberId = (int)cmbMembers.SelectedItem.GetType().GetProperty("Id")!.GetValue(cmbMembers.SelectedItem)!;
        _selectedMember = await _borrowService.GetMemberByIdAsync(memberId);

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

        var borrowCount = await _borrowService.GetActiveBorrowCountAsync(_selectedMember.Id);
        lblMemberBorrows.Text = $"Active Borrows: {borrowCount}";

        await LoadActiveBorrowsAsync();
    }

    private async Task LoadActiveBorrowsAsync()
    {
        if (_selectedMember == null)
        {
            dgvActiveBorrows.DataSource = null;
            _currentBorrows.Clear();
            ClearBorrowInfo();
            return;
        }

        _currentBorrows = await _borrowService.GetActiveBorrowsAsync(_selectedMember.Id);

        dgvActiveBorrows.DataSource = _currentBorrows.Select(br => new BorrowGridRow
        {
            BorrowRecordId = br.Id,
            BookTitle = br.BookCopy.Book.Title,
            Barcode = br.BookCopy.Barcode,
            BorrowDate = br.BorrowDate.ToLocalTime().ToString("yyyy-MM-dd"),
            DueDate = br.DueDate.ToLocalTime().ToString("yyyy-MM-dd"),
            RenewalCount = br.RenewalCount
        }).ToList();

        if (dgvActiveBorrows.Columns.Contains(nameof(BorrowGridRow.BorrowRecordId)))
        {
            var idColumn = dgvActiveBorrows.Columns[nameof(BorrowGridRow.BorrowRecordId)];
            if (idColumn != null)
                idColumn.Visible = false;
        }

        if (dgvActiveBorrows.Rows.Count > 0)
        {
            var selectedId = _initialBorrowRecordId;
            var targetRow = dgvActiveBorrows.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(r => r.DataBoundItem is BorrowGridRow row && selectedId.HasValue && row.BorrowRecordId == selectedId.Value)
                ?? dgvActiveBorrows.Rows[0];

            targetRow.Selected = true;
            SetCurrentCellToVisibleCell(targetRow);
            UpdateBorrowInfo();
        }
        else
        {
            ClearBorrowInfo();
        }

        _initialBorrowRecordId = null;
    }

    private void dgvActiveBorrows_SelectionChanged(object? sender, EventArgs e)
    {
        UpdateBorrowInfo();
    }

    private async void UpdateBorrowInfo()
    {
        if (dgvActiveBorrows.SelectedRows.Count == 0)
        {
            ClearBorrowInfo();
            return;
        }

        if (dgvActiveBorrows.SelectedRows[0].DataBoundItem is not BorrowGridRow row)
        {
            ClearBorrowInfo();
            return;
        }

        _selectedBorrowRecord = _currentBorrows.FirstOrDefault(br => br.Id == row.BorrowRecordId);
        if (_selectedBorrowRecord == null)
        {
            ClearBorrowInfo();
            return;
        }

        lblRenewBookTitle.Text = $"Book: {_selectedBorrowRecord.BookCopy.Book.Title}";
        lblRenewBorrowDate.Text = $"Borrowed: {_selectedBorrowRecord.BorrowDate.ToLocalTime():yyyy-MM-dd}";
        lblRenewDueDate.Text = $"Due date: {_selectedBorrowRecord.DueDate.ToLocalTime():yyyy-MM-dd}";
        lblRenewedCount.Text = $"Renewed: {_selectedBorrowRecord.RenewalCount}/2";
        lblRenewalError.Text = "";

        try
        {
            var remaining = await _renewalService.GetRenewalInfoAsync(_selectedBorrowRecord.Id);
            lblRemainingRenewals.Text = $"Remaining renewals: {remaining}";
            btnRenew.Enabled = remaining > 0;

            if (remaining == 0)
            {
                lblRenewalError.Text = "This borrow record has reached the renewal limit.";
            }
        }
        catch (Exception ex)
        {
            lblRemainingRenewals.Text = "Remaining renewals: -";
            btnRenew.Enabled = false;
            lblRenewalError.Text = ex.Message;
        }
    }

    private async void btnRenew_Click(object? sender, EventArgs e)
    {
        if (_selectedBorrowRecord == null)
        {
            lblRenewalError.Text = "Please select an active borrow record.";
            return;
        }

        var confirm = MessageBox.Show(
            $"Gia hạn '{_selectedBorrowRecord.BookCopy.Book.Title}' thêm 14 ngày?",
            "Confirm Renewal",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        var result = await _renewalService.RenewAsync(_selectedBorrowRecord.Id);

        if (result.Success)
        {
            MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var selectedId = _selectedBorrowRecord.Id;
            await LoadActiveBorrowsAsync();
            SelectBorrowRow(selectedId);
        }
        else
        {
            lblRenewalError.Text = result.Message;
        }
    }

    private void SelectBorrowRow(int borrowRecordId)
    {
        foreach (DataGridViewRow row in dgvActiveBorrows.Rows)
        {
            if (row.DataBoundItem is not BorrowGridRow gridRow)
                continue;

            if (gridRow.BorrowRecordId != borrowRecordId)
                continue;

            row.Selected = true;
            SetCurrentCellToVisibleCell(row);
            return;
        }
    }

    private void SetCurrentCellToVisibleCell(DataGridViewRow row)
    {
        var visibleCell = row.Cells
            .Cast<DataGridViewCell>()
            .FirstOrDefault(cell => cell.Visible && cell.OwningColumn.Visible);

        if (visibleCell != null)
        {
            dgvActiveBorrows.CurrentCell = visibleCell;
        }
    }

    private void ClearMemberInfo()
    {
        _selectedMember = null;
        lblMemberName.Text = "Name: -";
        lblMemberEmail.Text = "Email: -";
        lblMemberCard.Text = "Card: -";
        lblMemberBorrows.Text = "Active Borrows: -";
        dgvActiveBorrows.DataSource = null;
        _currentBorrows.Clear();
        ClearBorrowInfo();
    }

    private void ClearBorrowInfo()
    {
        _selectedBorrowRecord = null;
        lblRenewBookTitle.Text = "Book: -";
        lblRenewBorrowDate.Text = "Borrowed: -";
        lblRenewDueDate.Text = "Due date: -";
        lblRenewedCount.Text = "Renewed: -";
        lblRemainingRenewals.Text = "Remaining renewals: -";
        lblRenewalError.Text = "";
        btnRenew.Enabled = false;
    }

    private void btnBack_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private sealed class BorrowGridRow
    {
        public int BorrowRecordId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public string BorrowDate { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public int RenewalCount { get; set; }
    }
}
