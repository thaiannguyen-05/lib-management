using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Member
{
    public partial class MemberDetailForm : Form
    {
        private readonly MemberService _memberService;
        private Member? _member;
        private bool _isEditMode;

        public MemberDetailForm(MemberService memberService)
        {
            _memberService = memberService;
            InitializeComponent();
            Load += MemberDetailForm_Load;
        }

        public void SetMember(Member? member)
        {
            _member = member;
            _isEditMode = member != null;
        }

        private void MemberDetailForm_Load(object? sender, EventArgs e)
        {
            cboMemberType.DataSource = Enum.GetValues(typeof(MemberType));
            cboMemberType.SelectedIndex = 0;

            if (_isEditMode && _member != null)
            {
                this.Text = "Edit Member";
                lblTitle.Text = "Edit Member";
                PopulateFields();
            }
            else
            {
                this.Text = "Add Member";
                lblTitle.Text = "Add New Member";
            }

            UpdateDepartmentVisibility();
        }

        private void PopulateFields()
        {
            txtFirstName.Text = _member!.FirstName;
            txtLastName.Text = _member.LastName;
            txtEmail.Text = _member.Email;
            txtPhone.Text = _member.Phone ?? "";
            txtStudentId.Text = _member.StudentId ?? "";
            txtDepartment.Text = _member.Department ?? "";

            // Select the matching MemberType
            for (int i = 0; i < cboMemberType.Items.Count; i++)
            {
                if ((MemberType)cboMemberType.Items[i] == _member.MemberType)
                {
                    cboMemberType.SelectedIndex = i;
                    break;
                }
            }
        }

        private void cboMemberType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDepartmentVisibility();
        }

        private void UpdateDepartmentVisibility()
        {
            if (cboMemberType.SelectedItem == null) return;

            var selectedType = (MemberType)cboMemberType.SelectedItem;
            bool showDept = selectedType == MemberType.Student || selectedType == MemberType.Teacher;

            lblDepartment.Visible = showDept;
            txtDepartment.Visible = showDept;

            if (!showDept)
            {
                txtDepartment.Clear();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return;
            }

            var memberType = (MemberType)cboMemberType.SelectedItem!;
            var department = txtDepartment.Visible && !string.IsNullOrWhiteSpace(txtDepartment.Text)
                ? txtDepartment.Text.Trim()
                : null;
            var studentId = !string.IsNullOrWhiteSpace(txtStudentId.Text)
                ? txtStudentId.Text.Trim()
                : null;

            if (_isEditMode && _member != null)
            {
                // Update existing
                _member.FirstName = txtFirstName.Text.Trim();
                _member.LastName = txtLastName.Text.Trim();
                _member.Email = txtEmail.Text.Trim();
                _member.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
                _member.MemberType = memberType;
                _member.Department = department;
                _member.StudentId = studentId;

                var result = await _memberService.UpdateAsync(_member);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Create new
                var member = new Member
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                    MemberType = memberType,
                    Department = department,
                    StudentId = studentId,
                    Status = MemberStatus.Active
                };

                var result = await _memberService.CreateAsync(member);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
