namespace WinFormsApp1.Forms.Member
{
    partial class MemberListForm
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
            dgvMembers = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            FullName = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            MemberType = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            Department = new DataGridViewTextBoxColumn();
            StudentId = new DataGridViewTextBoxColumn();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnImportCsv = new Button();
            btnClear = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Member Management";
            // 
            // dgvMembers
            // 
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMembers.Columns.AddRange(new DataGridViewColumn[] { Id, FullName, Email, MemberType, Status, Department, StudentId });
            dgvMembers.Location = new Point(20, 50);
            dgvMembers.MultiSelect = false;
            dgvMembers.Name = "dgvMembers";
            dgvMembers.ReadOnly = true;
            dgvMembers.RowHeadersWidth = 51;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.Size = new Size(750, 300);
            dgvMembers.TabIndex = 1;
            dgvMembers.CellClick += dgvMembers_CellClick;
            // 
            // Id
            // 
            Id.HeaderText = "ID";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Width = 50;
            // 
            // FullName
            // 
            FullName.HeaderText = "Full Name";
            FullName.MinimumWidth = 6;
            FullName.Name = "FullName";
            FullName.ReadOnly = true;
            // 
            // Email
            // 
            Email.HeaderText = "Email";
            Email.MinimumWidth = 6;
            Email.Name = "Email";
            Email.ReadOnly = true;
            // 
            // MemberType
            // 
            MemberType.HeaderText = "Type";
            MemberType.MinimumWidth = 6;
            MemberType.Name = "MemberType";
            MemberType.ReadOnly = true;
            MemberType.Width = 80;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.Width = 80;
            // 
            // Department
            // 
            Department.HeaderText = "Department";
            Department.MinimumWidth = 6;
            Department.Name = "Department";
            Department.ReadOnly = true;
            // 
            // StudentId
            // 
            StudentId.HeaderText = "Student ID";
            StudentId.MinimumWidth = 6;
            StudentId.Name = "StudentId";
            StudentId.ReadOnly = true;
            StudentId.Width = 100;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(20, 370);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(52, 20);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(80, 367);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(400, 27);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(40, 167, 69);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 410);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 38);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(0, 122, 204);
            btnEdit.Enabled = false;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(150, 410);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 38);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.Enabled = false;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(280, 410);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 38);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnImportCsv
            // 
            btnImportCsv.BackColor = Color.FromArgb(255, 193, 7);
            btnImportCsv.FlatStyle = FlatStyle.Flat;
            btnImportCsv.ForeColor = Color.Black;
            btnImportCsv.Location = new Point(410, 410);
            btnImportCsv.Name = "btnImportCsv";
            btnImportCsv.Size = new Size(120, 38);
            btnImportCsv.TabIndex = 7;
            btnImportCsv.Text = "Import CSV";
            btnImportCsv.UseVisualStyleBackColor = false;
            btnImportCsv.Click += btnImportCsv_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(500, 365);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 30);
            btnClear.TabIndex = 8;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(640, 410);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(130, 38);
            btnBack.TabIndex = 9;
            btnBack.Text = "Back to Menu";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // MemberListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 470);
            Controls.Add(lblTitle);
            Controls.Add(dgvMembers);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnImportCsv);
            Controls.Add(btnClear);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MemberListForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Member Management";
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvMembers;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn FullName;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn MemberType;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn Department;
        private DataGridViewTextBoxColumn StudentId;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnImportCsv;
        private Button btnClear;
        private Button btnBack;
    }
}
