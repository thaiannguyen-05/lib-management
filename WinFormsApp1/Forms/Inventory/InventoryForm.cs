using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Inventory
{
    public partial class InventoryForm : Form
    {
        private readonly InventoryService _inventoryService;
        private readonly BookService _bookService;
        private readonly AppDbContext _context;

        public InventoryForm(
            InventoryService inventoryService,
            BookService bookService,
            AppDbContext context)
        {
            _inventoryService = inventoryService;
            _bookService = bookService;
            _context = context;
            InitializeComponent();
            Load += InventoryForm_Load;
        }

        private async void InventoryForm_Load(object? sender, EventArgs e)
        {
            await LoadSummaryAsync();
            await LoadLogsAsync();
            await LoadBooksAsync();
        }

        private async Task LoadSummaryAsync()
        {
            var counts = await _inventoryService.CountInventoryAsync();
            lblAvailable.Text = $"Available: {counts.GetValueOrDefault(CopyStatus.Available, 0)}";
            lblBorrowed.Text = $"Borrowed: {counts.GetValueOrDefault(CopyStatus.Borrowed, 0)}";
            lblDamaged.Text = $"Damaged: {counts.GetValueOrDefault(CopyStatus.Damaged, 0)}";
            lblLost.Text = $"Lost: {counts.GetValueOrDefault(CopyStatus.Lost, 0)}";
            lblTotal.Text = $"Total: {counts.Values.Sum()}";
        }

        private async Task LoadBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            cmbBook.DataSource = books.ToList();
            cmbBook.DisplayMember = "Title";
            cmbBook.ValueMember = "Id";
            cmbBook.SelectedIndex = -1;
        }

        private async Task LoadLogsAsync(int? bookCopyId = null, InventoryAction? action = null)
        {
            var logs = await _inventoryService.GetLogsAsync(bookCopyId, action);
            dgvLogs.DataSource = logs.Select(l => new
            {
                l.Id,
                Book = l.BookCopy?.Book?.Title ?? "N/A",
                Barcode = l.BookCopy?.Barcode ?? "N/A",
                Action = l.Action.ToString(),
                l.Quantity,
                Note = l.Note ?? "",
                By = l.PerformedByUser?.Username ?? "N/A",
                Date = l.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
            }).ToList();
        }

        private async Task<BookCopy?> FindCopyByBarcodeAsync(string barcode)
        {
            return await _context.BookCopies
                .FirstOrDefaultAsync(c => c.Barcode == barcode);
        }

        private async void btnImport_Click(object? sender, EventArgs e)
        {
            if (cmbBook.SelectedValue == null)
            {
                MessageBox.Show("Please select a book.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Enter a valid quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bookId = (int)cmbBook.SelectedValue;
            var userId = SessionManager.GetCurrentUserId();

            var copies = await _inventoryService.ImportBooksAsync(bookId, quantity, userId);
            MessageBox.Show($"Imported {copies.Count} copies.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtQuantity.Clear();
            await LoadSummaryAsync();
            await LoadLogsAsync();
        }

        private async void btnTransfer_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Enter a barcode.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewShelf.Text))
            {
                MessageBox.Show("Enter a new shelf location.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var copy = await FindCopyByBarcodeAsync(txtBarcode.Text.Trim());
            if (copy == null)
            {
                MessageBox.Show("Copy not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var success = await _inventoryService.TransferShelfAsync(copy.Id, txtNewShelf.Text.Trim(), SessionManager.GetCurrentUserId());
            if (success)
            {
                MessageBox.Show("Shelf transferred.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBarcode.Clear();
                txtNewShelf.Clear();
                await LoadLogsAsync();
            }
        }

        private async void btnDispose_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Enter a barcode.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var copy = await FindCopyByBarcodeAsync(txtBarcode.Text.Trim());
            if (copy == null)
            {
                MessageBox.Show("Copy not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show(
                $"Dispose copy '{copy.Barcode}'?",
                "Confirm Dispose", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var success = await _inventoryService.DisposeBookAsync(copy.Id, "Disposed by user", SessionManager.GetCurrentUserId());
            if (success)
            {
                MessageBox.Show("Copy disposed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBarcode.Clear();
                await LoadSummaryAsync();
                await LoadLogsAsync();
            }
        }

        private async void btnReportLost_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Enter a barcode.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var copy = await FindCopyByBarcodeAsync(txtBarcode.Text.Trim());
            if (copy == null)
            {
                MessageBox.Show("Copy not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var success = await _inventoryService.ReportLostAsync(copy.Id, SessionManager.GetCurrentUserId());
            if (success)
            {
                MessageBox.Show("Copy reported as lost.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBarcode.Clear();
                await LoadSummaryAsync();
                await LoadLogsAsync();
            }
        }

        private async void btnReportDamaged_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Enter a barcode.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var copy = await FindCopyByBarcodeAsync(txtBarcode.Text.Trim());
            if (copy == null)
            {
                MessageBox.Show("Copy not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var success = await _inventoryService.ReportDamagedAsync(copy.Id, SessionManager.GetCurrentUserId());
            if (success)
            {
                MessageBox.Show("Copy reported as damaged.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBarcode.Clear();
                await LoadSummaryAsync();
                await LoadLogsAsync();
            }
        }

        private async void cmbFilterAction_SelectedIndexChanged(object? sender, EventArgs e)
        {
            InventoryAction? action = cmbFilterAction.SelectedIndex > 0
                ? (InventoryAction)(cmbFilterAction.SelectedIndex - 1)
                : null;

            await LoadLogsAsync(action: action);
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            txtBarcode.Clear();
            txtNewShelf.Clear();
            txtQuantity.Clear();
            cmbBook.SelectedIndex = -1;
            cmbFilterAction.SelectedIndex = 0;
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
