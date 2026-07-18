using Microsoft.Extensions.DependencyInjection;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class MemberListForm : Form
    {
        private readonly MemberService _memberService;
        private readonly IServiceProvider _serviceProvider;
        private Member? _selectedMember;

        public MemberListForm(MemberService memberService, IServiceProvider serviceProvider)
        {
            _memberService = memberService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            Load += MemberListForm_Load;
        }

        private async void MemberListForm_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync(string? search = null)
        {
            var members = string.IsNullOrWhiteSpace(search)
                ? await _memberService.GetAllAsync()
                : await _memberService.SearchAsync(search);

            dgvMembers.DataSource = members;
            ClearSelection();
        }

        private void ClearSelection()
        {
            _selectedMember = null;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var detailForm = _serviceProvider.GetRequiredService<MemberDetailForm>();
            detailForm.SetMember(null);

            if (detailForm.ShowDialog(this) == DialogResult.OK)
            {
                await LoadDataAsync(txtSearch.Text);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedMember == null) return;

            var fullMember = await _memberService.GetByIdAsync(_selectedMember.Id);
            if (fullMember == null)
            {
                MessageBox.Show("Member not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var detailForm = _serviceProvider.GetRequiredService<MemberDetailForm>();
            detailForm.SetMember(fullMember);

            if (detailForm.ShowDialog(this) == DialogResult.OK)
            {
                await LoadDataAsync(txtSearch.Text);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedMember == null) return;

            var confirm = MessageBox.Show(
                $"Delete member '{_selectedMember.FirstName} {_selectedMember.LastName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = await _memberService.DeleteAsync(_selectedMember.Id);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await LoadDataAsync(txtSearch.Text);
        }

        private async void btnImportCsv_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Title = "Select CSV file to import";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var result = await _memberService.ImportFromCsvAsync(openFileDialog.FileName);
                MessageBox.Show(result.Message, "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadDataAsync(txtSearch.Text);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to import CSV: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvMembers.Rows[e.RowIndex];
            _selectedMember = new Member
            {
                Id = (int)row.Cells["Id"].Value,
                FirstName = row.Cells["FullName"].Value?.ToString()?.Split(' ')[0] ?? "",
                LastName = row.Cells["FullName"].Value?.ToString()?.Split(' ').Skip(1).FirstOrDefault() ?? "",
                Email = row.Cells["Email"].Value?.ToString() ?? ""
            };

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadDataAsync(txtSearch.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ClearSelection();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
