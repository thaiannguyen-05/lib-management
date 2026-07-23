using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class FeeForm : Form
    {
        private readonly LateFeeService _lateFeeService;
        private readonly FeePaymentService _feePaymentService;
        private readonly IUnitOfWork _unitOfWork;

        private int? _selectedLateFeeId;
        private List<LateFee> _currentFees = new();
        private bool _isLoadingMembers;

        public FeeForm(
            LateFeeService lateFeeService,
            FeePaymentService feePaymentService,
            IUnitOfWork unitOfWork)
        {
            _lateFeeService = lateFeeService;
            _feePaymentService = feePaymentService;
            _unitOfWork = unitOfWork;

            InitializeComponent();
            Load += FeeForm_Load;
        }

        private async void FeeForm_Load(object? sender, EventArgs e)
        {
            ConfigureGrid();
            ApplyPermissions();
            await LoadMembersAsync();
            UpdateSummary(Array.Empty<LateFee>());
        }

        private void ConfigureGrid()
        {
            dgvFees.AutoGenerateColumns = false;
            dgvFees.Columns.Clear();

            dgvFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLateFeeId",
                DataPropertyName = "LateFeeId",
                Visible = false
            });

            dgvFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBookTitle",
                HeaderText = "Tên sách",
                DataPropertyName = "BookTitle",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAmount",
                HeaderText = "Số tiền",
                DataPropertyName = "Amount",
                Width = 120,
                DefaultCellStyle = { Format = "N0" }
            });

            dgvFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDateIncurred",
                HeaderText = "Ngày phát sinh",
                DataPropertyName = "DateIncurred",
                Width = 150
            });

            dgvFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Trạng thái",
                DataPropertyName = "Status",
                Width = 120
            });
        }

        private async Task LoadMembersAsync()
        {
            _isLoadingMembers = true;
            try
            {
                var members = await _unitOfWork.Repository<Member>().GetAllAsync();
                var items = members
                    .OrderBy(m => m.FirstName)
                    .ThenBy(m => m.LastName)
                    .Select(m => new MemberComboItem(
                        m.Id,
                        $"{m.FirstName} {m.LastName} ({m.Email})"))
                    .ToList();

                cboMembers.DisplayMember = nameof(MemberComboItem.DisplayName);
                cboMembers.ValueMember = nameof(MemberComboItem.Id);
                cboMembers.DataSource = items;
                cboMembers.SelectedIndex = cboMembers.Items.Count > 0 ? 0 : -1;
            }
            finally
            {
                _isLoadingMembers = false;
            }
        }

        private void ApplyPermissions()
        {
            btnWaive.Visible = SessionManager.IsAdmin;
            lblAdminNote.Visible = !SessionManager.IsAdmin;
        }

        private async void cboMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingMembers)
            {
                return;
            }

            if (cboMembers.SelectedIndex < 0)
            {
                dgvFees.DataSource = null;
                UpdateSummary(Array.Empty<LateFee>());
                return;
            }

            if (cboMembers.SelectedItem is MemberComboItem selectedMember)
            {
                await LoadFeesAsync(selectedMember.Id);
            }
        }

        private async Task LoadFeesAsync(int memberId)
        {
            try
            {
                var fees = await _lateFeeService.GetAllByMemberAsync(memberId);
                _currentFees = fees;
                BindFees(fees);
            }
            catch (NotImplementedException ex)
            {
                dgvFees.DataSource = null;
                _currentFees = new List<LateFee>();
                UpdateSummary(Array.Empty<LateFee>());
                lblStatus.Text = ex.Message;
            }
            catch (Exception ex)
            {
                dgvFees.DataSource = null;
                _currentFees = new List<LateFee>();
                UpdateSummary(Array.Empty<LateFee>());
                lblStatus.Text = ex.Message;
            }
        }

        private void BindFees(IEnumerable<LateFee> fees)
        {
            var rows = fees.Select(f => new FeeGridRow
            {
                LateFeeId = f.Id,
                BookTitle = f.BorrowRecord?.BookCopy?.Book?.Title ?? "(Chưa tải dữ liệu sách)",
                Amount = f.Amount,
                DateIncurred = f.DateIncurred.ToLocalTime().ToString("yyyy-MM-dd"),
                Status = f.Status.ToString()
            }).ToList();

            dgvFees.DataSource = rows;
            _selectedLateFeeId = null;
            nudAmount.Value = 0;
            nudAmount.Maximum = decimal.MaxValue;
            btnPay.Enabled = false;
            btnWaive.Enabled = false;
            UpdateSummary(fees);
            lblStatus.Text = rows.Count == 0 ? "Không có phí cho thành viên này." : string.Empty;
        }

        private void UpdateSummary(IEnumerable<LateFee> fees)
        {
            var totalDebt = fees
                .Where(f => f.Status == FeeStatus.Unpaid)
                .Sum(f => f.Amount);

            lblTotalDebtValue.Text = totalDebt.ToString("N0");
        }

        private void dgvFees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvFees.Rows[e.RowIndex];
            _selectedLateFeeId = row.Cells["colLateFeeId"].Value == null
                ? null
                : Convert.ToInt32(row.Cells["colLateFeeId"].Value);

            if (!_selectedLateFeeId.HasValue)
            {
                btnPay.Enabled = false;
                btnWaive.Enabled = false;
                return;
            }

            var selectedFee = _currentFees.FirstOrDefault(f => f.Id == _selectedLateFeeId.Value);
            if (selectedFee == null)
            {
                btnPay.Enabled = false;
                btnWaive.Enabled = false;
                return;
            }

            var paidTotal = selectedFee.Payments.Sum(p => p.Amount);
            var remaining = Math.Max(0m, selectedFee.Amount - paidTotal);
            nudAmount.Maximum = Math.Max(0m, remaining);
            nudAmount.Value = remaining > 0 ? remaining : 0;

            btnPay.Enabled = remaining > 0 && selectedFee.Status == FeeStatus.Unpaid;
            btnWaive.Enabled = _selectedLateFeeId.HasValue && SessionManager.IsAdmin && selectedFee.Status == FeeStatus.Unpaid;
        }

        private async void btnPay_Click(object sender, EventArgs e)
        {
            if (!_selectedLateFeeId.HasValue)
            {
                MessageBox.Show("Chọn một khoản phí trước.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudAmount.Value <= 0)
            {
                MessageBox.Show("Nhập số tiền thanh toán hợp lệ.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _feePaymentService.PayAsync(_selectedLateFeeId.Value, nudAmount.Value);
                if (cboMembers.SelectedItem is MemberComboItem selectedMember)
                {
                    await LoadFeesAsync(selectedMember.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnWaive_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Chỉ Admin được miễn phạt.", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_selectedLateFeeId.HasValue)
            {
                MessageBox.Show("Chọn một khoản phí trước.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Miễn khoản phí đã chọn?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await _lateFeeService.WaiveAsync(_selectedLateFeeId.Value, SessionManager.GetCurrentUserId());
                if (cboMembers.SelectedItem is MemberComboItem selectedMember)
                {
                    await LoadFeesAsync(selectedMember.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Waive", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cboMembers.SelectedItem is MemberComboItem selectedMember)
            {
                await LoadFeesAsync(selectedMember.Id);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private sealed class FeeGridRow
        {
            public int LateFeeId { get; set; }
            public string BookTitle { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public string DateIncurred { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
        }

        private sealed class MemberComboItem
        {
            public MemberComboItem(int id, string displayName)
            {
                Id = id;
                DisplayName = displayName;
            }

            public int Id { get; }
            public string DisplayName { get; }
        }
    }
}
