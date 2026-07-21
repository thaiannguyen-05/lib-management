using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApp1.Forms.Auth;
using WinFormsApp1.Forms.Books;
using WinFormsApp1.Forms.Authors;
using WinFormsApp1.Forms.Categories;
using WinFormsApp1.Forms.Publishers;
using WinFormsApp1.Forms.Members;
using WinFormsApp1.Forms.Reservations;
using WinFormsApp1.Forms.Inventory;
using WinFormsApp1.Forms.Report;
using WinFormsApp1.Forms.User;
using WinFormsApp1.Forms.Borrow;
using WinFormsApp1.Forms.Return;
using WinFormsApp1.Helpers;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Main
{
    public partial class MainForm : Form
    {
        private readonly AuditService _auditService;
        private readonly IServiceProvider _serviceProvider;
        private LoginForm? _ownerLoginForm;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LoginForm? OwnerLoginForm
        {
            get => _ownerLoginForm;
            set => _ownerLoginForm = value;
        }

        public MainForm(AuditService auditService, IServiceProvider serviceProvider)
        {
            _auditService = auditService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            UpdateUIForRole();
            this.FormClosing += MainForm_FormClosing;
        }

        private void UpdateUIForRole()
        {
            if (!SessionManager.IsLoggedIn) return;

            var user = SessionManager.CurrentUser!;
            lblWelcome.Text = $"Welcome, {user.Username} ({user.Role})";

            btnUsers.Visible = SessionManager.IsAdmin;
            btnBooks.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnMembers.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnAuthors.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnCategories.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnLibraryCards.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnPublishers.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnInventory.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnReports.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsLoggedIn)
            {
                await _auditService.LogAsync(
                    SessionManager.GetCurrentUserId(),
                    "Logout",
                    "ApplicationUser",
                    SessionManager.GetCurrentUserId(),
                    $"User '{SessionManager.CurrentUser!.Username}' logged out");

                SessionManager.Logout();
            }

            if (OwnerLoginForm != null)
            {
                OwnerLoginForm.ResetForm();
                OwnerLoginForm.Show();
                OwnerLoginForm.BringToFront();
            }
            this.Close();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            var bookForm = _serviceProvider.GetRequiredService<BookForm>();
            bookForm.ShowDialog(this);
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            var memberListForm = _serviceProvider.GetRequiredService<MemberListForm>();
            memberListForm.ShowDialog(this);
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            var borrowForm = _serviceProvider.GetRequiredService<BorrowForm>();
            borrowForm.ShowDialog(this);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            var userForm = _serviceProvider.GetRequiredService<UserManageForm>();
            userForm.ShowDialog(this);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            var reportForm = _serviceProvider.GetRequiredService<ReportForm>();
            reportForm.ShowDialog(this);
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            var authorForm = _serviceProvider.GetRequiredService<AuthorForm>();
            authorForm.ShowDialog(this);
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            var categoryForm = _serviceProvider.GetRequiredService<CategoryForm>();
            categoryForm.ShowDialog(this);
        }

        private void btnLibraryCards_Click(object sender, EventArgs e)
        {
            var libraryCardForm = _serviceProvider.GetRequiredService<LibraryCardForm>();
            libraryCardForm.ShowDialog(this);
        }

        private void btnPublishers_Click(object sender, EventArgs e)
        {
            var publisherForm = _serviceProvider.GetRequiredService<PublisherForm>();
            publisherForm.ShowDialog(this);
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            var reservationForm = _serviceProvider.GetRequiredService<ReservationForm>();
            reservationForm.ShowDialog(this);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var changePasswordForm = _serviceProvider.GetRequiredService<ChangePasswordForm>();
            changePasswordForm.ShowDialog(this);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            var inventoryForm = _serviceProvider.GetRequiredService<InventoryForm>();
            inventoryForm.ShowDialog(this);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            var returnForm = _serviceProvider.GetRequiredService<ReturnForm>();
            returnForm.ShowDialog(this);
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (SessionManager.IsLoggedIn)
            {
                SessionManager.Logout();
            }

            Application.Exit();
        }
    }
}
