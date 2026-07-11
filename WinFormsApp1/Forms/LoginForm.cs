using Microsoft.Extensions.DependencyInjection;
using WinFormsApp1.Helpers;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        public LoginForm(IAuthService authService, IServiceProvider serviceProvider)
        {
            _authService = authService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            lblStatus.Text = "Logging in...";
            lblStatus.ForeColor = SystemColors.ControlText;

            try
            {
                var user = await _authService.LoginAsync(username, password);

                if (user == null)
                {
                    lblStatus.Text = "Invalid username or password.";
                    lblStatus.ForeColor = Color.Red;
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                SessionManager.Login(user);

                var mainForm = _serviceProvider.GetRequiredService<MainForm>();
                mainForm.OwnerLoginForm = this;
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        public void ResetForm()
        {
            lblStatus.Text = string.Empty;
            txtPassword.Clear();
            btnLogin.Enabled = true;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SessionManager.IsLoggedIn)
            {
                SessionManager.Logout();
            }

            Application.Exit();
        }
    }
}
