using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Report
{
    public partial class ReportForm : Form
    {
        private readonly ReportService _reportService;

        public ReportForm(ReportService reportService)
        {
            _reportService = reportService;
            InitializeComponent();
            Load += ReportForm_Load;
        }

        private async void ReportForm_Load(object? sender, EventArgs e)
        {
            dtpFrom.Checked = false;
            dtpTo.Checked = false;
            await LoadAllReportsAsync();
        }

        private async Task LoadAllReportsAsync()
        {
            DateTime? from = dtpFrom.Checked ? dtpFrom.Value.Date : null;
            DateTime? to = dtpTo.Checked ? dtpTo.Value.Date.AddDays(1).AddTicks(-1) : null;

            await LoadCurrentlyBorrowedAsync(from, to);
            await LoadOverdueBooksAsync(from, to);
            await LoadMostBorrowedBooksAsync(from, to);
            await LoadMostActiveMembersAsync(from, to);
            await LoadTotalFeesAsync(from, to);
            await LoadLostDamagedBooksAsync(from, to);
        }

        private async Task LoadCurrentlyBorrowedAsync(DateTime? from, DateTime? to)
        {
            var data = await _reportService.GetCurrentlyBorrowedAsync(from, to);
            dgvBorrowed.DataSource = data.Select(x => new
            {
                BookTitle = GetProp(x, "BookTitle"),
                Barcode = GetProp(x, "Barcode"),
                MemberName = GetProp(x, "MemberName"),
                BorrowDate = GetDateProp(x, "BorrowDate"),
                DueDate = GetDateProp(x, "DueDate")
            }).ToList();
        }

        private async Task LoadOverdueBooksAsync(DateTime? from, DateTime? to)
        {
            var data = await _reportService.GetOverdueBooksAsync(from, to);
            dgvOverdue.DataSource = data.Select(x => new
            {
                BookTitle = GetProp(x, "BookTitle"),
                Barcode = GetProp(x, "Barcode"),
                MemberName = GetProp(x, "MemberName"),
                DueDate = GetDateProp(x, "DueDate"),
                DaysOverdue = GetIntProp(x, "DaysOverdue")
            }).ToList();
        }

        private async Task LoadMostBorrowedBooksAsync(DateTime? from, DateTime? to)
        {
            var data = await _reportService.GetMostBorrowedBooksAsync(10, from, to);
            dgvMostBorrowed.DataSource = data.Select(x => new
            {
                BookTitle = GetProp(x, "BookTitle"),
                BorrowCount = GetIntProp(x, "BorrowCount")
            }).ToList();
        }

        private async Task LoadMostActiveMembersAsync(DateTime? from, DateTime? to)
        {
            var data = await _reportService.GetMostActiveMembersAsync(10, from, to);
            dgvMostActive.DataSource = data.Select(x => new
            {
                MemberName = GetProp(x, "MemberName"),
                BorrowCount = GetIntProp(x, "BorrowCount")
            }).ToList();
        }

        private async Task LoadTotalFeesAsync(DateTime? from, DateTime? to)
        {
            var total = await _reportService.GetTotalFeesAsync(from, to);
            lblTotalFees.Text = $"Tổng tiền phạt: {total:N0} VNĐ";
        }

        private async Task LoadLostDamagedBooksAsync(DateTime? from, DateTime? to)
        {
            var data = await _reportService.GetLostDamagedBooksAsync(from, to);
            dgvLostDamaged.DataSource = data.Select(x => new
            {
                BookTitle = GetProp(x, "BookTitle"),
                Barcode = GetProp(x, "Barcode"),
                Status = GetProp(x, "Status"),
                ShelfLocation = GetProp(x, "ShelfLocation")
            }).ToList();
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            await LoadAllReportsAsync();
        }

        private void btnExportCsv_Click(object? sender, EventArgs e)
        {
            if (tabReports.SelectedTab == null) return;

            var dgv = tabReports.SelectedTab.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = $"Report_{tabReports.SelectedTab.Text}_{DateTime.Now:yyyyMMdd}.csv";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var writer = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                var headers = dgv.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText);
                writer.WriteLine(string.Join(",", headers));

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var values = row.Cells.Cast<DataGridViewCell>()
                        .Select(c => EscapeCsv(c.Value?.ToString() ?? ""));
                    writer.WriteLine(string.Join(",", values));
                }

                MessageBox.Show("Xuất báo cáo thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string EscapeCsv(string value)
        {
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }

        private static string GetProp(object obj, string name)
        {
            return obj.GetType().GetProperty(name)?.GetValue(obj)?.ToString() ?? "";
        }

        private static string GetDateProp(object obj, string name)
        {
            var val = obj.GetType().GetProperty(name)?.GetValue(obj);
            if (val is DateTime dt)
                return dt.ToString("dd/MM/yyyy");
            return val?.ToString() ?? "";
        }

        private static int GetIntProp(object obj, string name)
        {
            var val = obj.GetType().GetProperty(name)?.GetValue(obj);
            if (val is int i) return i;
            if (int.TryParse(val?.ToString(), out int result)) return result;
            return 0;
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
