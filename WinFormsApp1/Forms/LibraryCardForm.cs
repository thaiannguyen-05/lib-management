using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class LibraryCardForm : Form
    {
        private readonly LibraryCardService _libraryCardService;
        private readonly IUnitOfWork _unitOfWork;
        private LibraryCard? _currentCard;

        public LibraryCardForm(LibraryCardService libraryCardService, IUnitOfWork unitOfWork)
        {
            _libraryCardService = libraryCardService;
            _unitOfWork = unitOfWork;
            InitializeComponent();
            Load += LibraryCardForm_Load;
        }

        private async void LibraryCardForm_Load(object? sender, EventArgs e)
        {
            await LoadMembersAsync();
            ApplyRolePermissions();
            ClearCardInfo();
        }

        private async Task LoadMembersAsync()
        {
            var members = await _unitOfWork.Repository<Member>().GetAllAsync();
            cboMembers.DataSource = members.Select(m => new
            {
                m.Id,
                DisplayName = $"{m.FirstName} {m.LastName}"
            }).ToList();
            cboMembers.DisplayMember = "DisplayName";
            cboMembers.ValueMember = "Id";
            cboMembers.SelectedIndex = -1;
        }

        private void ApplyRolePermissions()
        {
            var role = SessionManager.GetCurrentUserRole();

            btnIssue.Visible = role == UserRole.Admin || role == UserRole.Librarian;

            bool canRenew = role == UserRole.Admin || role == UserRole.Librarian;
            lblRenew.Visible = canRenew;
            btnRenew1M.Visible = canRenew;
            btnRenew3M.Visible = canRenew;
            btnRenew6M.Visible = canRenew;
            btnRenew1Y.Visible = canRenew;

            btnLockUnlock.Visible = role == UserRole.Admin;
        }

        private async void cboMembers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboMembers.SelectedIndex < 0)
            {
                ClearCardInfo();
                return;
            }

            int memberId = (int)cboMembers.SelectedValue;
            _currentCard = await _libraryCardService.GetByMemberIdAsync(memberId);
            UpdateCardDisplay();
        }

        private void ClearCardInfo()
        {
            _currentCard = null;
            lblCardNumber.Text = "";
            lblExpiryDate.Text = "";
            lblStatus.Text = "";
            lblWarning.Visible = false;
            UpdateButtonVisibility();
        }

        private void UpdateCardDisplay()
        {
            if (_currentCard == null)
            {
                lblCardNumber.Text = "No card issued";
                lblExpiryDate.Text = "-";
                lblStatus.Text = "-";
                lblWarning.Visible = false;
            }
            else
            {
                lblCardNumber.Text = _currentCard.CardNumber;
                lblExpiryDate.Text = _currentCard.ExpiryDate.ToString("yyyy-MM-dd");
                lblStatus.Text = _currentCard.Status.ToString();

                var daysUntilExpiry = (_currentCard.ExpiryDate - DateTime.Now).Days;
                if (daysUntilExpiry <= 30 && daysUntilExpiry >= 0)
                {
                    lblWarning.Text = $"Card expires in {daysUntilExpiry} day(s)!";
                    lblWarning.Visible = true;
                }
                else if (daysUntilExpiry < 0)
                {
                    lblWarning.Text = "Card has expired!";
                    lblWarning.Visible = true;
                }
                else
                {
                    lblWarning.Visible = false;
                }
            }

            UpdateButtonVisibility();
        }

        private void UpdateButtonVisibility()
        {
            bool hasCard = _currentCard != null;
            bool isActive = _currentCard?.Status == CardStatus.Active;

            btnIssue.Visible = !hasCard && (SessionManager.IsAdmin || SessionManager.IsLibrarian);

            bool canRenew = hasCard && isActive && (SessionManager.IsAdmin || SessionManager.IsLibrarian);
            lblRenew.Visible = canRenew;
            btnRenew1M.Visible = canRenew;
            btnRenew3M.Visible = canRenew;
            btnRenew6M.Visible = canRenew;
            btnRenew1Y.Visible = canRenew;

            if (hasCard && SessionManager.IsAdmin)
            {
                btnLockUnlock.Visible = true;
                btnLockUnlock.Text = isActive ? "Lock Card" : "Unlock Card";
                btnLockUnlock.BackColor = isActive ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69);
            }
            else
            {
                btnLockUnlock.Visible = false;
            }
        }

        private async void btnIssue_Click(object sender, EventArgs e)
        {
            if (cboMembers.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a member.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberId = (int)cboMembers.SelectedValue;
            var result = await _libraryCardService.IssueCardAsync(memberId);

            if (result.Success)
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            _currentCard = await _libraryCardService.GetByMemberIdAsync(memberId);
            UpdateCardDisplay();
        }

        private async void RenewCard(RenewalPeriod period)
        {
            if (_currentCard == null) return;

            var result = await _libraryCardService.RenewCardAsync(_currentCard.Id, period);

            if (result.Success)
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (cboMembers.SelectedIndex >= 0)
            {
                int memberId = (int)cboMembers.SelectedValue;
                _currentCard = await _libraryCardService.GetByMemberIdAsync(memberId);
                UpdateCardDisplay();
            }
        }

        private void btnRenew1M_Click(object sender, EventArgs e) => RenewCard(RenewalPeriod.OneMonth);
        private void btnRenew3M_Click(object sender, EventArgs e) => RenewCard(RenewalPeriod.ThreeMonths);
        private void btnRenew6M_Click(object sender, EventArgs e) => RenewCard(RenewalPeriod.SixMonths);
        private void btnRenew1Y_Click(object sender, EventArgs e) => RenewCard(RenewalPeriod.TwelveMonths);

        private async void btnLockUnlock_Click(object sender, EventArgs e)
        {
            if (_currentCard == null) return;

            var confirm = MessageBox.Show(
                _currentCard.Status == CardStatus.Active
                    ? "Lock this card? The member won't be able to borrow books."
                    : "Unlock this card?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = _currentCard.Status == CardStatus.Active
                ? await _libraryCardService.LockCardAsync(_currentCard.Id)
                : await _libraryCardService.UnlockCardAsync(_currentCard.Id);

            if (result.Success)
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (cboMembers.SelectedIndex >= 0)
            {
                int memberId = (int)cboMembers.SelectedValue;
                _currentCard = await _libraryCardService.GetByMemberIdAsync(memberId);
                UpdateCardDisplay();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
