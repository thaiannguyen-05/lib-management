namespace WinFormsApp1.Forms.Members
{
    partial class MemberDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblMemberType = new Label();
            cboMemberType = new ComboBox();
            lblDepartment = new Label();
            txtDepartment = new TextBox();
            lblStudentId = new Label();
            txtStudentId = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(450, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add New Member";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(20, 60);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(75, 20);
            lblFirstName.TabIndex = 1;
            lblFirstName.Text = "First Name:";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(140, 57);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(300, 27);
            txtFirstName.TabIndex = 2;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(20, 100);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(75, 20);
            lblLastName.TabIndex = 3;
            lblLastName.Text = "Last Name:";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(140, 97);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(300, 27);
            txtLastName.TabIndex = 4;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 140);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(140, 137);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 27);
            txtEmail.TabIndex = 6;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(20, 180);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(49, 20);
            lblPhone.TabIndex = 7;
            lblPhone.Text = "Phone:";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(140, 177);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(300, 27);
            txtPhone.TabIndex = 8;
            // 
            // lblMemberType
            // 
            lblMemberType.AutoSize = true;
            lblMemberType.Location = new Point(20, 220);
            lblMemberType.Name = "lblMemberType";
            lblMemberType.Size = new Size(98, 20);
            lblMemberType.TabIndex = 9;
            lblMemberType.Text = "Member Type:";
            // 
            // cboMemberType
            // 
            cboMemberType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMemberType.Location = new Point(140, 217);
            cboMemberType.Name = "cboMemberType";
            cboMemberType.Size = new Size(300, 28);
            cboMemberType.TabIndex = 10;
            cboMemberType.SelectedIndexChanged += cboMemberType_SelectedIndexChanged;
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(20, 260);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(85, 20);
            lblDepartment.TabIndex = 11;
            lblDepartment.Text = "Department:";
            // 
            // txtDepartment
            // 
            txtDepartment.Location = new Point(140, 257);
            txtDepartment.Name = "txtDepartment";
            txtDepartment.Size = new Size(300, 27);
            txtDepartment.TabIndex = 12;
            // 
            // lblStudentId
            // 
            lblStudentId.AutoSize = true;
            lblStudentId.Location = new Point(20, 300);
            lblStudentId.Name = "lblStudentId";
            lblStudentId.Size = new Size(75, 20);
            lblStudentId.TabIndex = 13;
            lblStudentId.Text = "Student ID:";
            // 
            // txtStudentId
            // 
            txtStudentId.Location = new Point(140, 297);
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Size = new Size(300, 27);
            txtStudentId.TabIndex = 14;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(140, 340);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 40);
            btnSave.TabIndex = 15;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(300, 340);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 40);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // MemberDetailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 400);
            Controls.Add(lblTitle);
            Controls.Add(lblFirstName);
            Controls.Add(txtFirstName);
            Controls.Add(lblLastName);
            Controls.Add(txtLastName);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblMemberType);
            Controls.Add(cboMemberType);
            Controls.Add(lblDepartment);
            Controls.Add(txtDepartment);
            Controls.Add(lblStudentId);
            Controls.Add(txtStudentId);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MemberDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New Member";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblMemberType;
        private ComboBox cboMemberType;
        private Label lblDepartment;
        private TextBox txtDepartment;
        private Label lblStudentId;
        private TextBox txtStudentId;
        private Button btnSave;
        private Button btnCancel;
    }
}
