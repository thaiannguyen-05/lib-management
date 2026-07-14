using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms;

public partial class BookCopyForm : Form
{
    private readonly BookService _bookService;
    private readonly int _bookId;
    private readonly string _bookTitle;
    private BookCopy? _selectedCopy;

    public BookCopyForm(BookService bookService, int bookId, string bookTitle)
    {
        _bookService = bookService;
        _bookId = bookId;
        _bookTitle = bookTitle;
        InitializeComponent();
        Load += BookCopyForm_Load;
    }

    private async void BookCopyForm_Load(object? sender, EventArgs e)
    {
        lblBookTitle.Text = $"Copies of: {_bookTitle}";
        cmbStatus.DataSource = Enum.GetValues(typeof(CopyStatus));
        cmbStatus.SelectedIndex = 0;
        await LoadCopiesAsync();
    }

    private async Task LoadCopiesAsync()
    {
        var copies = await _bookService.GetCopiesByBookIdAsync(_bookId);
        dgvCopies.DataSource = copies.Select(c => new
        {
            c.Id,
            Status = c.Status.ToString(),
            Created = c.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd")
        }).ToList();

        ClearForm();
    }

    private void ClearForm()
    {
        _selectedCopy = null;
        cmbStatus.SelectedIndex = 0;
        btnAdd.Text = "Add New Copy";
        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
    }

    private void PopulateFormWithCopy(BookCopy copy)
    {
        _selectedCopy = copy;
        cmbStatus.SelectedItem = copy.Status;
        btnAdd.Text = "Update Copy";
        btnEdit.Enabled = true;
        btnDelete.Enabled = true;
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        var copy = new BookCopy
        {
            BookId = _bookId,
            Status = (CopyStatus)cmbStatus.SelectedItem!
        };

        await _bookService.AddBookCopyAsync(copy);
        MessageBox.Show("Copy added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        await LoadCopiesAsync();
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        if (_selectedCopy == null) return;
        btnAdd.Text = "Save Changes";
        btnEdit.Enabled = false;
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedCopy == null) return;

        var confirm = MessageBox.Show(
            $"Delete copy #{_selectedCopy.Id}?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        var success = await _bookService.DeleteBookCopyAsync(_selectedCopy.Id);
        if (success)
        {
            MessageBox.Show("Copy deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Cannot delete copy with active borrow records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        await LoadCopiesAsync();
    }

    private async void btnSave_Click(object? sender, EventArgs e)
    {
        if (_selectedCopy == null) return;

        _selectedCopy.Status = (CopyStatus)cmbStatus.SelectedItem!;
        await _bookService.UpdateBookCopyAsync(_selectedCopy);
        MessageBox.Show("Copy updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        await LoadCopiesAsync();
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        ClearForm();
    }

    private void btnBack_Click(object? sender, EventArgs e)
    {
        this.Close();
    }

    private void dgvCopies_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvCopies.CurrentRow == null) return;

        var copyId = Convert.ToInt32(dgvCopies.CurrentRow.Cells["Id"].Value);
        var status = Enum.Parse<CopyStatus>(dgvCopies.CurrentRow.Cells["Status"].Value?.ToString() ?? "Available");
        _selectedCopy = new BookCopy { Id = copyId, BookId = _bookId, Status = status };
        cmbStatus.SelectedItem = status;
        btnEdit.Enabled = true;
        btnDelete.Enabled = true;
        btnAdd.Text = "Update Copy";
    }
}
