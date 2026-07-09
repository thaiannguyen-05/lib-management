using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class AuthorForm : Form
    {
        private readonly IAuthorService _authorService;
        private Author? _selectedAuthor;

        public AuthorForm(IAuthorService authorService)
        {
            _authorService = authorService;
            InitializeComponent();
            Load += AuthorForm_Load;
        }

        private async void AuthorForm_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
            ClearForm();
        }

        private async Task LoadDataAsync(string? search = null)
        {
            var authors = string.IsNullOrWhiteSpace(search)
                ? await _authorService.GetAllAsync()
                : await _authorService.SearchByNameAsync(search);

            dgvAuthors.DataSource = authors.Select(a => new
            {
                a.Id,
                a.FirstName,
                a.LastName,
                a.Bio,
                Created = a.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd")
            }).ToList();
        }

        private void ClearForm()
        {
            _selectedAuthor = null;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtBio.Clear();
            txtFirstName.Focus();
            btnAdd.Text = "Add";
            btnDelete.Enabled = false;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First Name and Last Name are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedAuthor != null)
            {
                _selectedAuthor.FirstName = txtFirstName.Text.Trim();
                _selectedAuthor.LastName = txtLastName.Text.Trim();
                _selectedAuthor.Bio = txtBio.Text.Trim();
                await _authorService.UpdateAsync(_selectedAuthor);
                MessageBox.Show("Author updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var author = new Author
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Bio = txtBio.Text.Trim()
                };
                await _authorService.CreateAsync(author);
                MessageBox.Show("Author added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            await LoadDataAsync(txtSearch.Text);
            ClearForm();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedAuthor == null) return;

            var confirm = MessageBox.Show(
                $"Delete author '{_selectedAuthor.FirstName} {_selectedAuthor.LastName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var success = await _authorService.DeleteAsync(_selectedAuthor.Id);
            if (success)
            {
                MessageBox.Show("Author deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cannot delete author with existing books.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await LoadDataAsync(txtSearch.Text);
            ClearForm();
        }

        private void dgvAuthors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvAuthors.Rows[e.RowIndex];
            _selectedAuthor = new Author
            {
                Id = (int)row.Cells["Id"].Value,
                FirstName = row.Cells["FirstName"].Value?.ToString() ?? "",
                LastName = row.Cells["LastName"].Value?.ToString() ?? "",
                Bio = row.Cells["Bio"].Value?.ToString()
            };

            txtFirstName.Text = _selectedAuthor.FirstName;
            txtLastName.Text = _selectedAuthor.LastName;
            txtBio.Text = _selectedAuthor.Bio ?? "";
            btnAdd.Text = "Update";
            btnDelete.Enabled = true;
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadDataAsync(txtSearch.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ClearForm();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
