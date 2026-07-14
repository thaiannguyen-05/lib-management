using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms;

public partial class BookForm : Form
{
    private readonly BookService _bookService;
    private Book? _selectedBook;
    private bool _isEditing;

    public BookForm(BookService bookService)
    {
        _bookService = bookService;
        InitializeComponent();
        Load += BookForm_Load;
    }

    private async void BookForm_Load(object? sender, EventArgs e)
    {
        await LoadPublishersAsync();
        await LoadAuthorsAsync();
        await LoadCategoriesAsync();
        await LoadBooksAsync();
        ClearForm();
    }

    private async Task LoadBooksAsync(string? search = null)
    {
        var books = string.IsNullOrWhiteSpace(search)
            ? await _bookService.GetAllBooksAsync()
            : await _bookService.SearchBooksAsync(search);

        dgvBooks.DataSource = books.Select(b => new
        {
            b.Id,
            b.Title,
            b.ISBN,
            Publisher = b.Publisher?.Name ?? "N/A",
            b.PublicationYear,
            b.ShelfLocation,
            b.ReplacementCost,
            Authors = string.Join(", ", b.Authors.Select(a => $"{a.FirstName} {a.LastName}")),
            Categories = string.Join(", ", b.Categories.Select(c => c.Name)),
            AvailableCopies = b.Copies.Count(c => c.Status == Models.Enums.CopyStatus.Available),
            TotalCopies = b.Copies.Count
        }).ToList();
    }

    private async Task LoadPublishersAsync()
    {
        var publishers = await _bookService.GetAllPublishersAsync();
        cmbPublisher.DataSource = publishers.ToList();
        cmbPublisher.DisplayMember = "Name";
        cmbPublisher.ValueMember = "Id";
        cmbPublisher.SelectedIndex = -1;
    }

    private async Task LoadAuthorsAsync()
    {
        var authors = await _bookService.GetAllAuthorsAsync();
        cmbAuthor.DataSource = authors.ToList();
        cmbAuthor.DisplayMember = "FullName";
        cmbAuthor.ValueMember = "Id";
        cmbAuthor.SelectedIndex = -1;
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await _bookService.GetAllCategoriesAsync();
        clbCategories.DataSource = categories.ToList();
        clbCategories.DisplayMember = "Name";
        clbCategories.ValueMember = "Id";
    }

    private async Task LoadBookCopiesAsync(int bookId)
    {
        var copies = await _bookService.GetCopiesByBookIdAsync(bookId);
        dgvCopies.DataSource = copies.Select(c => new
        {
            c.Id,
            Status = c.Status.ToString()
        }).ToList();
    }

    private void ClearForm()
    {
        _selectedBook = null;
        _isEditing = false;
        txtTitle.Clear();
        txtISBN.Clear();
        txtDescription.Clear();
        txtShelfLocation.Clear();
        txtSearch.Clear();
        nudPublicationYear.Value = DateTime.Now.Year;
        nudReplacementCost.Value = 0;
        cmbPublisher.SelectedIndex = -1;
        cmbAuthor.SelectedIndex = -1;

        for (int i = 0; i < clbCategories.Items.Count; i++)
            clbCategories.SetItemChecked(i, false);

        dgvCopies.DataSource = null;
        lblCopies.Text = "Book Copies";
        btnAdd.Text = "Add";
        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
        btnSave.Enabled = false;
        btnCancel.Enabled = false;
        pnlForm.Enabled = false;
        txtTitle.Focus();
    }

    private void PopulateFormWithBook(Book book)
    {
        _selectedBook = book;
        _isEditing = true;
        txtTitle.Text = book.Title;
        txtISBN.Text = book.ISBN;
        txtDescription.Text = book.Description ?? "";
        txtShelfLocation.Text = book.ShelfLocation ?? "";
        nudPublicationYear.Value = book.PublicationYear ?? DateTime.Now.Year;
        nudReplacementCost.Value = book.ReplacementCost;

        if (book.PublisherId.HasValue)
            cmbPublisher.SelectedValue = book.PublisherId.Value;
        else
            cmbPublisher.SelectedIndex = -1;

        if (book.Authors.Any())
            cmbAuthor.SelectedValue = book.Authors.First().Id;
        else
            cmbAuthor.SelectedIndex = -1;

        for (int i = 0; i < clbCategories.Items.Count; i++)
        {
            var category = (Category)clbCategories.Items[i];
            clbCategories.SetItemChecked(i, book.Categories.Any(c => c.Id == category.Id));
        }

        btnAdd.Text = "Update";
        btnEdit.Enabled = true;
        btnDelete.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        pnlForm.Enabled = true;
    }

    private int[] GetSelectedAuthorIds()
    {
        if (cmbAuthor.SelectedValue is int id)
            return new[] { id };
        return Array.Empty<int>();
    }

    private int[] GetSelectedCategoryIds()
    {
        return clbCategories.CheckedItems
            .Cast<Category>()
            .Select(c => c.Id)
            .ToArray();
    }

    private async void btnAdd_Click(object? sender, EventArgs e)
    {
        pnlForm.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        _isEditing = false;
        _selectedBook = null;
        btnAdd.Text = "New";
        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
        txtTitle.Clear();
        txtISBN.Clear();
        txtDescription.Clear();
        txtShelfLocation.Clear();
        nudPublicationYear.Value = DateTime.Now.Year;
        nudReplacementCost.Value = 0;
        cmbPublisher.SelectedIndex = -1;
        cmbAuthor.SelectedIndex = -1;
        for (int i = 0; i < clbCategories.Items.Count; i++)
            clbCategories.SetItemChecked(i, false);
        txtTitle.Focus();
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        if (_selectedBook == null) return;
        PopulateFormWithBook(_selectedBook);
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedBook == null) return;

        var confirm = MessageBox.Show(
            $"Delete book '{_selectedBook.Title}'?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        var success = await _bookService.DeleteBookAsync(_selectedBook.Id);
        if (success)
        {
            MessageBox.Show("Book deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Cannot delete book with borrowed copies.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        await LoadBooksAsync(txtSearch.Text);
        ClearForm();
    }

    private async void btnSave_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtISBN.Text))
        {
            MessageBox.Show("ISBN is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var authorIds = GetSelectedAuthorIds();
        var categoryIds = GetSelectedCategoryIds();

        var book = new Book
        {
            Title = txtTitle.Text.Trim(),
            ISBN = txtISBN.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            ShelfLocation = txtShelfLocation.Text.Trim(),
            PublicationYear = (int)nudPublicationYear.Value,
            ReplacementCost = nudReplacementCost.Value,
            PublisherId = cmbPublisher.SelectedValue as int?
        };

        if (_isEditing && _selectedBook != null)
        {
            book.Id = _selectedBook.Id;
            await _bookService.UpdateBookAsync(book, authorIds, categoryIds);
            MessageBox.Show("Book updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            await _bookService.AddBookAsync(book, authorIds, categoryIds);
            MessageBox.Show("Book added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        await LoadBooksAsync(txtSearch.Text);
        ClearForm();
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        ClearForm();
    }

    private async void btnSearch_Click(object? sender, EventArgs e)
    {
        await LoadBooksAsync(txtSearch.Text);
    }

    private async void btnManageCopies_Click(object? sender, EventArgs e)
    {
        if (_selectedBook == null)
        {
            MessageBox.Show("Please select a book first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var copyForm = new BookCopyForm(_bookService, _selectedBook.Id, _selectedBook.Title);
        copyForm.ShowDialog(this);
        if (_selectedBook != null)
        {
            await LoadBookCopiesAsync(_selectedBook.Id);
            await LoadBooksAsync(txtSearch.Text);
        }
    }

    private async void dgvBooks_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvBooks.CurrentRow == null) return;

        var bookId = Convert.ToInt32(dgvBooks.CurrentRow.Cells["Id"].Value);
        var book = await _bookService.GetBookByIdAsync(bookId);
        if (book != null)
        {
            _selectedBook = book;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnManageCopies.Enabled = true;
            lblCopies.Text = $"Book Copies (Available: {book.Copies.Count(c => c.Status == Models.Enums.CopyStatus.Available)}, Total: {book.Copies.Count})";
            await LoadBookCopiesAsync(bookId);
        }
    }

    private async void txtSearch_TextChanged(object? sender, EventArgs e)
    {
        await LoadBooksAsync(txtSearch.Text);
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        txtSearch.Clear();
        ClearForm();
    }

    private void btnBack_Click(object? sender, EventArgs e)
    {
        this.Close();
    }
}
