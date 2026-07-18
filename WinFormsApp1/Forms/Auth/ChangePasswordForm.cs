using WinFormsApp1.Helpers;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Auth
{
    public partial class ChangePasswordForm : Form
    {
        private readonly AuthService _authService;
        private readonly AuditService _auditService;

        public ChangePasswordForm(AuthService authService, AuditService auditService)
        {
            _authService = authService;
            _auditService = auditService;
            InitializeComponent();
        }

        private async void btnChange_Click(object sender, EventArgs e)
        {
            var oldPassword = txtOldPassword.Text;
            var newPassword = txtNewPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnChange.Enabled = false;

            try
            {
                var userId = SessionManager.GetCurrentUserId();
                var (success, message) = await _authService.ChangePasswordAsync(userId, oldPassword, newPassword);

                if (success)
                {
                    await _auditService.LogAsync(
                        userId,
                        "ChangePassword",
                        "ApplicationUser",
                        userId,
                        "User changed their password");

                    MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOldPassword.Clear();
                    txtOldPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnChange.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
