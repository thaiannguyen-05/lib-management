using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Borrow;

public partial class BorrowForm : Form
{
    private readonly BorrowService _borrowService;
    private readonly AppDbContext _context;
    private Member? _selectedMember;
    private BookCopy? _selectedBookCopy;

    public BorrowForm(BorrowService borrowService, AppDbContext context)
    {
        _borrowService = borrowService;
        _context = context;
        InitializeComponent();
        Load += BorrowForm_Load;
    }

    private async void BorrowForm_Load(object? sender, EventArgs e)
    {
        await LoadMembersAsync();
    }

    private async Task LoadMembersAsync()
    {
        var members = await _context.Members
            .Where(m => m.Status == Models.Enums.MemberStatus.Active)
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
        if (cmbMembers.SelectedIndex < 0)
        {
            ClearMemberInfo();
            return;
        }

        var selected = cmbMembers.SelectedItem;
        var memberId = (int)selected.GetType().GetProperty("Id")!.GetValue(selected)!;

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
        ValidateForm();
    }

    private async void btnSearchBook_Click(object? sender, EventArgs e)
    {
        var keyword = txtBookSearch.Text.Trim();
        if (string.IsNullOrWhiteSpace(keyword))
        {
            lblError.Text = "Please enter a barcode or ISBN.";
            return;
        }

        lblError.Text = "";
        _selectedBookCopy = await _borrowService.SearchBookCopyAsync(keyword);

        if (_selectedBookCopy == null)
        {
            ClearBookInfo();
            lblError.Text = "Book copy not found.";
            return;
        }

        var authors = string.Join(", ", _selectedBookCopy.Book.Authors.Select(a => $"{a.FirstName} {a.LastName}"));
        lblBookTitle.Text = $"Title: {_selectedBookCopy.Book.Title}";
        lblBookBarcode.Text = $"Barcode: {_selectedBookCopy.Barcode} (Status: {_selectedBookCopy.Status})";
        lblBookAuthors.Text = $"Authors: {(string.IsNullOrEmpty(authors) ? "N/A" : authors)}";
        lblBookShelf.Text = $"Shelf: {_selectedBookCopy.ShelfLocation ?? "N/A"}";

        ValidateForm();
    }

    private async void btnBorrow_Click(object? sender, EventArgs e)
    {
        if (_selectedMember == null || _selectedBookCopy == null) return;

        var result = await _borrowService.CheckoutAsync(
            _selectedMember.Id,
            _selectedBookCopy.Id,
            SessionManager.GetCurrentUserId());

        if (result.Success)
        {
            MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset book search
            _selectedBookCopy = null;
            txtBookSearch.Text = "";
            ClearBookInfo();

            // Refresh member borrow count and list
            var borrowCount = await _borrowService.GetActiveBorrowCountAsync(_selectedMember.Id);
            lblMemberBorrows.Text = $"Active Borrows: {borrowCount}";
            await LoadActiveBorrowsAsync();

            ValidateForm();
        }
        else
        {
            lblError.Text = result.Message;
        }
    }

    private async Task LoadActiveBorrowsAsync()
    {
        if (_selectedMember == null)
        {
            dgvActiveBorrows.DataSource = null;
            return;
        }

        var borrows = await _borrowService.GetActiveBorrowsAsync(_selectedMember.Id);
        dgvActiveBorrows.DataSource = borrows.Select(br => new
        {
            br.Id,
            BookTitle = br.BookCopy.Book.Title,
            br.BookCopy.Barcode,
            BorrowDate = br.BorrowDate.ToLocalTime().ToString("yyyy-MM-dd"),
            DueDate = br.DueDate.ToLocalTime().ToString("yyyy-MM-dd"),
            br.RenewalCount
        }).ToList();
    }

    private void ValidateForm()
    {
        bool memberValid = _selectedMember != null && _selectedMember.Status == Models.Enums.MemberStatus.Active;
        bool bookValid = _selectedBookCopy != null && _selectedBookCopy.Status == Models.Enums.CopyStatus.Available;
        btnBorrow.Enabled = memberValid && bookValid;

        if (_selectedBookCopy != null && _selectedBookCopy.Status != Models.Enums.CopyStatus.Available)
        {
            lblError.Text = "This copy is not available for borrowing.";
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

    private void btnBack_Click(object? sender, EventArgs e)
    {
        this.Close();
    }
}
