using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApp1.Helpers;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class MainForm : Form
    {
        private readonly IAuditService _auditService;
        private readonly IServiceProvider _serviceProvider;
        private LoginForm? _ownerLoginForm;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LoginForm? OwnerLoginForm
        {
            get => _ownerLoginForm;
            set => _ownerLoginForm = value;
        }

        public MainForm(IAuditService auditService, IServiceProvider serviceProvider)
        {
            _auditService = auditService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            UpdateUIForRole();
        }

        private void UpdateUIForRole()
        {
            if (!SessionManager.IsLoggedIn) return;

            var user = SessionManager.CurrentUser!;
            lblWelcome.Text = $"Welcome, {user.Username} ({user.Role})";

            btnUsers.Visible = SessionManager.IsAdmin;
            btnAuthors.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
            btnCategories.Visible = SessionManager.IsAdmin || SessionManager.IsLibrarian;
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
            MessageBox.Show("Books module - Coming in Milestone 3", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Members module - Coming in Milestone 4", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Borrowing module - Coming in Milestone 5", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("User management - Coming in Milestone 2 (extension)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports module - Coming in Milestone 7", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var changePasswordForm = _serviceProvider.GetRequiredService<ChangePasswordForm>();
            changePasswordForm.ShowDialog(this);
        }
    }
}
