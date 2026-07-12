using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class UserManageForm : Form
    {
        private readonly IUserService _userService;
        private ApplicationUser? _selectedUser;

        public UserManageForm(IUserService userService)
        {
            _userService = userService;
            InitializeComponent();
            Load += UserManageForm_Load;
        }

        private async void UserManageForm_Load(object? sender, EventArgs e)
        {
            cmbRole.DataSource = Enum.GetValues(typeof(UserRole));
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var users = await _userService.GetAllAsync();
            dgvUsers.DataSource = users.Select(u => new
            {
                u.Id,
                u.Username,
                u.Role,
                Created = u.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd")
            }).ToList();
        }

        private void ClearForm()
        {
            _selectedUser = null;
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = 0;
            txtUsername.Focus();
            btnAdd.Text = "Add";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvUsers.Rows[e.RowIndex];
            _selectedUser = new ApplicationUser
            {
                Id = (int)row.Cells["Id"].Value,
                Username = row.Cells["Username"].Value?.ToString() ?? "",
                Role = (UserRole)row.Cells["Role"].Value
            };

            txtUsername.Text = _selectedUser.Username;
            cmbRole.SelectedItem = _selectedUser.Role;
            btnAdd.Text = "Add";
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new ApplicationUser
            {
                Username = txtUsername.Text.Trim(),
                Role = (UserRole)cmbRole.SelectedItem!
            };

            var (success, message) = await _userService.CreateAsync(user, txtPassword.Text);
            MessageBox.Show(message, success ? "Success" : "Error", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success)
            {
                await LoadDataAsync();
                ClearForm();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null) return;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedUser.Username = txtUsername.Text.Trim();
            _selectedUser.Role = (UserRole)cmbRole.SelectedItem!;

            var (success, message) = await _userService.UpdateAsync(_selectedUser, null);
            MessageBox.Show(message, success ? "Success" : "Error", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success)
            {
                await LoadDataAsync();
                ClearForm();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null) return;

            var confirm = MessageBox.Show(
                $"Delete user '{_selectedUser.Username}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var (success, message) = await _userService.DeleteAsync(_selectedUser.Id);
            MessageBox.Show(message, success ? "Success" : "Error", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success)
            {
                await LoadDataAsync();
                ClearForm();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
