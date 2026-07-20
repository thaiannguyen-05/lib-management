using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Publishers
{
    public partial class PublisherForm : Form
    {
        private readonly PublisherService _publisherService;
        private Publisher? _selectedPublisher;

        public PublisherForm(PublisherService publisherService)
        {
            _publisherService = publisherService;
            InitializeComponent();
            Load += PublisherForm_Load;
        }

        private async void PublisherForm_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
            ClearForm();
        }

        private async Task LoadDataAsync(string? search = null)
        {
            var publishers = string.IsNullOrWhiteSpace(search)
                ? await _publisherService.GetAllAsync()
                : await _publisherService.SearchByNameAsync(search);

            dgvPublishers.DataSource = publishers.Select(p => new
            {
                p.Id,
                p.Name,
                p.Address,
                p.Phone,
                Created = p.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd")
            }).ToList();
        }

        private void ClearForm()
        {
            _selectedPublisher = null;
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
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

            if (_selectedPublisher != null)
            {
                _selectedPublisher.Name = txtName.Text.Trim();
                _selectedPublisher.Address = txtAddress.Text.Trim();
                _selectedPublisher.Phone = txtPhone.Text.Trim();
                await _publisherService.UpdateAsync(_selectedPublisher);
                MessageBox.Show("Publisher updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var publisher = new Publisher
                {
                    Name = txtName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                };
                await _publisherService.CreateAsync(publisher);
                MessageBox.Show("Publisher added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            await LoadDataAsync(txtSearch.Text);
            ClearForm();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPublisher == null) return;

            var confirm = MessageBox.Show(
                $"Delete publisher '{_selectedPublisher.Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var success = await _publisherService.DeleteAsync(_selectedPublisher.Id);
            if (success)
            {
                MessageBox.Show("Publisher deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cannot delete publisher with existing books.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await LoadDataAsync(txtSearch.Text);
            ClearForm();
        }

        private void dgvPublishers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPublishers.Rows[e.RowIndex];
            _selectedPublisher = new Publisher
            {
                Id = (int)row.Cells["Id"].Value,
                Name = row.Cells["Name"].Value?.ToString() ?? "",
                Address = row.Cells["Address"].Value?.ToString(),
                Phone = row.Cells["Phone"].Value?.ToString()
            };

            txtName.Text = _selectedPublisher.Name;
            txtAddress.Text = _selectedPublisher.Address ?? "";
            txtPhone.Text = _selectedPublisher.Phone ?? "";
            btnAdd.Text = "Update";
            btnDelete.Enabled = true;
        }
        //Khi user go hoac xoa 1 ki tu se co thay doi theo thoi gian thuc
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
            Close();
        }
    }
}
