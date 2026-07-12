using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class CategoryForm : Form
    {
        private readonly CategoryService _categoryService;
        private Category? _selectedCategory;

        public CategoryForm(CategoryService categoryService)
        {
            _categoryService = categoryService;
            InitializeComponent();
            Load += CategoryForm_Load;
        }

        private async void CategoryForm_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
            ClearForm();
        }

        private async Task LoadDataAsync(string? search = null)
        {
            var categories = string.IsNullOrWhiteSpace(search)
                ? await _categoryService.GetAllAsync()
                : await _categoryService.SearchByNameAsync(search);

            dgvCategories.DataSource = categories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Description,
                Created = c.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd")
            }).ToList();
        }

        private void ClearForm()
        {
            _selectedCategory = null;
            txtName.Clear();
            txtDescription.Clear();
            txtName.Focus();
            btnAdd.Text = "Add";
            btnDelete.Enabled = false;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedCategory != null)
            {
                _selectedCategory.Name = txtName.Text.Trim();
                _selectedCategory.Description = txtDescription.Text.Trim();
                await _categoryService.UpdateAsync(_selectedCategory);
                MessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var category = new Category
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };
                await _categoryService.CreateAsync(category);
                MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            await LoadDataAsync(txtSearch.Text);
            ClearForm();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCategory == null) return;

            var confirm = MessageBox.Show(
                $"Delete category '{_selectedCategory.Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var success = await _categoryService.DeleteAsync(_selectedCategory.Id);
            if (success)
            {
                MessageBox.Show("Category deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cannot delete category with existing books.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await LoadDataAsync(txtSearch.Text);
            ClearForm();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvCategories.Rows[e.RowIndex];
            _selectedCategory = new Category
            {
                Id = (int)row.Cells["Id"].Value,
                Name = row.Cells["Name"].Value?.ToString() ?? "",
                Description = row.Cells["Description"].Value?.ToString()
            };

            txtName.Text = _selectedCategory.Name;
            txtDescription.Text = _selectedCategory.Description ?? "";
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
