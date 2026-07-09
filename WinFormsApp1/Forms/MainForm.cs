using Microsoft.Extensions.DependencyInjection;
using WinFormsApp1.Helpers;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class MainForm : Form
    {
        private readonly IAuditService _auditService;

        public MainForm(IAuditService auditService)
        {
            _auditService = auditService;
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

            var loginForm = Program.ServiceProvider!.GetRequiredService<LoginForm>();
            loginForm.FormClosed += (_, _) => this.Close();
            loginForm.Show();
            this.Hide();
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
            var authorForm = Program.ServiceProvider!.GetRequiredService<AuthorForm>();
            authorForm.ShowDialog(this);
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            var categoryForm = Program.ServiceProvider!.GetRequiredService<CategoryForm>();
            categoryForm.ShowDialog(this);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var changePasswordForm = Program.ServiceProvider!.GetRequiredService<ChangePasswordForm>();
            changePasswordForm.ShowDialog(this);
        }
    }
}
