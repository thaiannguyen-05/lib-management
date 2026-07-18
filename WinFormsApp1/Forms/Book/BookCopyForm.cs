using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Book;

public partial class BookCopyForm : Form
{
    private readonly BookCopyService _bookCopyService;
    private readonly int _bookId;
    private readonly string _bookTitle;
    private BookCopy? _selectedCopy;

    public BookCopyForm(BookCopyService bookCopyService, int bookId, string bookTitle)
    {
        _bookCopyService = bookCopyService;
        _bookId = bookId;
        _bookTitle = bookTitle;
        InitializeComponent();
        Load += BookCopyForm_Load;
    }

    private async void BookCopyForm_Load(object? sender, EventArgs e)
    {
        lblBookTitle.Text = $"Copies of: {_bookTitle}";

        // Setup Deck combo: A, B, C
        cmbDeck.Items.AddRange(new object[] { "A", "B", "C" });
        cmbDeck.SelectedIndex = 0;

        // Setup Row combo: 01, 02, 03, 04
        cmbRow.Items.AddRange(new object[] { "01", "02", "03", "04" });
        cmbRow.SelectedIndex = 0;

        // Setup Floor combo: 01, 02, 03, 04
        cmbFloor.Items.AddRange(new object[] { "01", "02", "03", "04" });
        cmbFloor.SelectedIndex = 0;

        // Setup Status combo
        cmbStatus.DataSource = Enum.GetValues(typeof(CopyStatus));
        cmbStatus.SelectedIndex = 0;

        await LoadCopiesAsync();
    }

    // Build shelf location string from 3 combos: Deck-Row-Floor
    private string BuildShelfLocation()
    {
        string deck = cmbDeck.SelectedItem?.ToString() ?? "A";
        string row = cmbRow.SelectedItem?.ToString() ?? "01";
        string floor = cmbFloor.SelectedItem?.ToString() ?? "01";
        return $"{deck}-{row}-{floor}";
    }

    // Parse shelf location string and set the 3 combos
    private void SetShelfLocation(string shelf)
    {
        // Expected format: A-01-02
        var parts = shelf.Split('-');
        if (parts.Length == 3)
        {
            cmbDeck.SelectedItem = parts[0];
            cmbRow.SelectedItem = parts[1];
            cmbFloor.SelectedItem = parts[2];
        }
    }

    // Load all copies of this book into the grid
    private async Task LoadCopiesAsync()
    {
        var copies = await _bookCopyService.GetByBookIdAsync(_bookId);
        dgvCopies.DataSource = copies.Select(c => new
        {
            c.Id,
            c.Barcode,
            c.ShelfLocation,
            Status = c.Status.ToString(),
            Created = c.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd")
        }).ToList();

        ClearForm();
    }

    private void ClearForm()
    {
        _selectedCopy = null;
        cmbDeck.SelectedIndex = 0;
        cmbRow.SelectedIndex = 0;
        cmbFloor.SelectedIndex = 0;
        cmbStatus.SelectedIndex = 0;
        btnAddCopy.Text = "Add Copy";
        btnChangeStatus.Enabled = false;
        btnDelete.Enabled = false;
    }

    // Select a copy from the grid
    private void dgvCopies_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvCopies.CurrentRow == null) return;

        var copyId = Convert.ToInt32(dgvCopies.CurrentRow.Cells["Id"].Value);
        var statusText = dgvCopies.CurrentRow.Cells["Status"].Value?.ToString() ?? "Available";
        var shelf = dgvCopies.CurrentRow.Cells["ShelfLocation"].Value?.ToString() ?? "A-01-01";

        _selectedCopy = new BookCopy
        {
            Id = copyId,
            BookId = _bookId,
            Status = Enum.Parse<CopyStatus>(statusText),
            ShelfLocation = shelf
        };

        SetShelfLocation(shelf);
        cmbStatus.SelectedItem = _selectedCopy.Status;
        btnChangeStatus.Enabled = true;

        // Only enable Delete if status is Available
        btnDelete.Enabled = _selectedCopy.Status == CopyStatus.Available;
    }

    // Add a new copy
    private async void btnAddCopy_Click(object? sender, EventArgs e)
    {
        var copy = new BookCopy
        {
            BookId = _bookId,
            ShelfLocation = BuildShelfLocation(),
            Status = (CopyStatus)cmbStatus.SelectedItem!
        };

        await _bookCopyService.CreateAsync(copy);
        MessageBox.Show($"Copy added! Barcode: {copy.Barcode}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        await LoadCopiesAsync();
    }

    // Change status and shelf location of selected copy
    private async void btnChangeStatus_Click(object? sender, EventArgs e)
    {
        if (_selectedCopy == null) return;

        var newStatus = (CopyStatus)cmbStatus.SelectedItem!;
        var newShelf = BuildShelfLocation();

        await _bookCopyService.UpdateStatusAsync(_selectedCopy.Id, newStatus);
        await _bookCopyService.UpdateShelfLocationAsync(_selectedCopy.Id, newShelf);

        MessageBox.Show("Copy updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        await LoadCopiesAsync();
    }

    // Delete selected copy (only if Available)
    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (_selectedCopy == null) return;

        if (_selectedCopy.Status != CopyStatus.Available)
        {
            MessageBox.Show("Only copies with 'Available' status can be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var confirm = MessageBox.Show(
            $"Delete copy #{_selectedCopy.Id} (Barcode: {_selectedCopy.Barcode})?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        var success = await _bookCopyService.DeleteAsync(_selectedCopy.Id);
        if (success)
            MessageBox.Show("Copy deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        else
            MessageBox.Show("Cannot delete this copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        await LoadCopiesAsync();
    }

    private void btnBack_Click(object? sender, EventArgs e)
    {
        this.Close();
    }
}
