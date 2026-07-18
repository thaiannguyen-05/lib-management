using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class ReservationForm : Form
    {
        private readonly ReservationService _reservationService;
        private readonly BookService _bookService;
        private readonly MemberService _memberService;
        private Reservation? _selectedReservation;

        public ReservationForm(
            ReservationService reservationService,
            BookService bookService,
            MemberService memberService)
        {
            _reservationService = reservationService;
            _bookService = bookService;
            _memberService = memberService;
            InitializeComponent();
            Load += ReservationForm_Load;
        }

        private async void ReservationForm_Load(object? sender, EventArgs e)
        {
            await LoadBooksAsync();
            await LoadMembersAsync();
            await LoadReservationsAsync();
            ClearSelection();
        }

        private async Task LoadReservationsAsync()
        {
            var reservations = await _reservationService.GetAllAsync();
            dgvReservations.DataSource = reservations.Select(r => new
            {
                r.Id,
                BookTitle = r.Book?.Title ?? "N/A",
                MemberName = r.Member != null ? $"{r.Member.FirstName} {r.Member.LastName}" : "N/A",
                ReservationDate = r.ReservationDate.ToString("yyyy-MM-dd HH:mm"),
                Status = r.Status.ToString(),
                ExpiryDate = r.ExpiryDate?.ToString("yyyy-MM-dd HH:mm") ?? "-"
            }).ToList();
        }

        private async Task LoadBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            cmbBook.DataSource = books.ToList();
            cmbBook.DisplayMember = "Title";
            cmbBook.ValueMember = "Id";
            cmbBook.SelectedIndex = -1;
        }

        private async Task LoadMembersAsync()
        {
            var members = await _memberService.GetAllAsync();
            cmbMember.DataSource = members.ToList();
            cmbMember.DisplayMember = "FullName";
            cmbMember.ValueMember = "Id";
            cmbMember.SelectedIndex = -1;
        }

        private void ClearSelection()
        {
            _selectedReservation = null;
            btnReserve.Enabled = false;
            btnCancel.Enabled = false;
            btnFulfill.Enabled = false;
            btnCollect.Enabled = false;
            btnComplete.Enabled = false;
        }

        private async void btnReserve_Click(object sender, EventArgs e)
        {
            if (cmbBook.SelectedValue == null || cmbMember.SelectedValue == null)
            {
                MessageBox.Show("Please select both a book and a member.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bookId = (int)cmbBook.SelectedValue;
            var memberId = (int)cmbMember.SelectedValue;

            var result = await _reservationService.CreateAsync(bookId, memberId);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadReservationsAsync();
                ClearSelection();
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            if (_selectedReservation == null) return;

            var confirm = MessageBox.Show(
                $"Cancel reservation #{_selectedReservation.Id}?",
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = await _reservationService.CancelAsync(_selectedReservation.Id);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadReservationsAsync();
                ClearSelection();
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnFulfill_Click(object sender, EventArgs e)
        {
            if (_selectedReservation == null) return;

            var result = await _reservationService.FulfillAsync(_selectedReservation.Id);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadReservationsAsync();
                ClearSelection();
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCollect_Click(object sender, EventArgs e)
        {
            if (_selectedReservation == null) return;

            var result = await _reservationService.CollectAsync(_selectedReservation.Id);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadReservationsAsync();
                ClearSelection();
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnComplete_Click(object sender, EventArgs e)
        {
            if (_selectedReservation == null) return;

            var result = await _reservationService.CompleteAsync(_selectedReservation.Id);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadReservationsAsync();
                ClearSelection();
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvReservations.Rows[e.RowIndex];
            _selectedReservation = new Reservation
            {
                Id = (int)row.Cells["Id"].Value,
                Book = new Book { Title = row.Cells["BookTitle"].Value?.ToString() ?? "" },
                Member = new Member
                {
                    FirstName = row.Cells["MemberName"].Value?.ToString()?.Split(' ')[0] ?? "",
                    LastName = row.Cells["MemberName"].Value?.ToString()?.Split(' ').Skip(1).FirstOrDefault() ?? ""
                }
            };

            var status = row.Cells["Status"].Value?.ToString();
            btnReserve.Enabled = false;
            btnCancel.Enabled = status == "Pending" || status == "Ready";
            btnFulfill.Enabled = status == "Pending";
            btnCollect.Enabled = status == "Ready";
            btnComplete.Enabled = status == "Collected";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
