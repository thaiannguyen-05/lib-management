using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Borrow;

public partial class BorrowForm : Form
{
    private readonly BorrowService _borrowService;
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    private Member? _selectedMember;
    private BookCopy? _selectedBookCopy;
    private List<BorrowRecord> _currentBorrows = new();
    private BorrowRecord? _selectedBorrowRecord;

    public BorrowForm(BorrowService borrowService, AppDbContext context, IServiceProvider serviceProvider)
    {
        _borrowService = borrowService;
        _context = context;
        _serviceProvider = serviceProvider;
        InitializeComponent();
        Load += BorrowForm_Load;
    }

    private async void BorrowForm_Load(object? sender, EventArgs e)
    {
        await LoadMembersAsync();
        ClearSelectionInfo();
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
        ValidateBorrowForm();
    }

    private async void btnSearchBook_Click(object? sender, EventArgs e)
    {
        var keyword = txtBookSearch.Text.Trim();
        if (string.IsNullOrWhiteSpace(keyword))
        {
            lblBorrowError.Text = "Please enter a barcode or ISBN.";
            return;
        }

        lblBorrowError.Text = "";
        _selectedBookCopy = await _borrowService.SearchBookCopyAsync(keyword);

        if (_selectedBookCopy == null)
        {
            ClearBookInfo();
            lblBorrowError.Text = "Book copy not found.";
            return;
        }

        var authors = string.Join(", ", _selectedBookCopy.Book.Authors.Select(a => $"{a.FirstName} {a.LastName}"));
        lblBookTitle.Text = $"Title: {_selectedBookCopy.Book.Title}";
        lblBookBarcode.Text = $"Barcode: {_selectedBookCopy.Barcode} (Status: {_selectedBookCopy.Status})";
        lblBookAuthors.Text = $"Authors: {(string.IsNullOrEmpty(authors) ? "N/A" : authors)}";
        lblBookShelf.Text = $"Shelf: {_selectedBookCopy.Book.ShelfLocation ?? "N/A"}";

        ValidateBorrowForm();
    }

    private async void btnBorrow_Click(object? sender, EventArgs e)
    {
        if (_selectedMember == null || _selectedBookCopy == null)
            return;

        var result = await _borrowService.CheckoutAsync(
            _selectedMember.Id,
            _selectedBookCopy.Id,
            SessionManager.GetCurrentUserId());

        if (result.Success)
        {
            MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _selectedBookCopy = null;
            txtBookSearch.Text = string.Empty;
            ClearBookInfo();

            var borrowCount = await _borrowService.GetActiveBorrowCountAsync(_selectedMember.Id);
            lblMemberBorrows.Text = $"Active Borrows: {borrowCount}";

            await LoadActiveBorrowsAsync();
            ValidateBorrowForm();
        }
        else
        {
            lblBorrowError.Text = result.Message;
        }
    }

    private async Task LoadActiveBorrowsAsync()
    {
        if (_selectedMember == null)
        {
            dgvActiveBorrows.DataSource = null;
            _currentBorrows.Clear();
            _selectedBorrowRecord = null;
            lblSelectedBorrow.Text = "Selected Borrow: -";
            btnOpenRenewForm.Enabled = false;
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
            dgvActiveBorrows.ClearSelection();
            dgvActiveBorrows.Rows[0].Selected = true;
            SetSelectedBorrowFromGrid();
        }
        else
        {
            _selectedBorrowRecord = null;
            lblSelectedBorrow.Text = "Selected Borrow: -";
            btnOpenRenewForm.Enabled = false;
        }
    }

    private void dgvActiveBorrows_SelectionChanged(object? sender, EventArgs e)
    {
        SetSelectedBorrowFromGrid();
    }

    private void SetSelectedBorrowFromGrid()
    {
        if (dgvActiveBorrows.SelectedRows.Count == 0)
        {
            _selectedBorrowRecord = null;
            lblSelectedBorrow.Text = "Selected Borrow: -";
            btnOpenRenewForm.Enabled = false;
            return;
        }

        if (dgvActiveBorrows.SelectedRows[0].DataBoundItem is not BorrowGridRow row)
        {
            _selectedBorrowRecord = null;
            lblSelectedBorrow.Text = "Selected Borrow: -";
            btnOpenRenewForm.Enabled = false;
            return;
        }

        _selectedBorrowRecord = _currentBorrows.FirstOrDefault(br => br.Id == row.BorrowRecordId);
        if (_selectedBorrowRecord == null)
        {
            lblSelectedBorrow.Text = "Selected Borrow: -";
            btnOpenRenewForm.Enabled = false;
            return;
        }

        lblSelectedBorrow.Text = $"Selected Borrow: {_selectedBorrowRecord.BookCopy.Book.Title} | Due: {_selectedBorrowRecord.DueDate.ToLocalTime():yyyy-MM-dd}";
        btnOpenRenewForm.Enabled = true;
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

    private void btnOpenRenewForm_Click(object? sender, EventArgs e)
    {
        using var scope = _serviceProvider.CreateScope();
        var renewForm = scope.ServiceProvider.GetRequiredService<RenewForm>();
        renewForm.InitializeSelection(_selectedMember?.Id, _selectedBorrowRecord?.Id);
        renewForm.ShowDialog(this);
        if (_selectedMember != null)
        {
            _ = RefreshAfterRenewAsync();
        }
    }

    private async Task RefreshAfterRenewAsync()
    {
        if (_selectedMember == null)
            return;

        var borrowCount = await _borrowService.GetActiveBorrowCountAsync(_selectedMember.Id);
        lblMemberBorrows.Text = $"Active Borrows: {borrowCount}";
        await LoadActiveBorrowsAsync();
        ValidateBorrowForm();
    }

    private void ValidateBorrowForm()
    {
        var memberValid = _selectedMember != null && _selectedMember.Status == MemberStatus.Active;
        var bookValid = _selectedBookCopy != null && _selectedBookCopy.Status == CopyStatus.Available;
        btnBorrow.Enabled = memberValid && bookValid;

        if (_selectedBookCopy != null && _selectedBookCopy.Status != CopyStatus.Available)
        {
            lblBorrowError.Text = "This copy is not available for borrowing.";
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
        _selectedBorrowRecord = null;
        lblSelectedBorrow.Text = "Selected Borrow: -";
        btnOpenRenewForm.Enabled = false;
        btnBorrow.Enabled = false;
    }

    private void ClearBookInfo()
    {
        _selectedBookCopy = null;
        lblBookTitle.Text = "Title: -";
        lblBookBarcode.Text = "Barcode: -";
        lblBookAuthors.Text = "Authors: -";
        lblBookShelf.Text = "Shelf: -";
        btnBorrow.Enabled = false;
    }

    private void ClearSelectionInfo()
    {
        ClearBookInfo();
        ClearMemberInfo();
        lblBorrowError.Text = string.Empty;
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
