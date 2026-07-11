namespace WinFormsApp1.Forms
{
    partial class AuthorForm
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
            dgvAuthors = new DataGridView();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblBio = new Label();
            txtBio = new TextBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnBack = new Button();
            lblSearch = new Label();
            txtSearch = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvAuthors).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Author Management";
            // 
            // dgvAuthors
            // 
            dgvAuthors.AllowUserToAddRows = false;
            dgvAuthors.AllowUserToDeleteRows = false;
            dgvAuthors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuthors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuthors.Location = new Point(20, 50);
            dgvAuthors.MultiSelect = false;
            dgvAuthors.Name = "dgvAuthors";
            dgvAuthors.ReadOnly = true;
            dgvAuthors.RowHeadersWidth = 51;
            dgvAuthors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuthors.Size = new Size(750, 300);
            dgvAuthors.TabIndex = 1;
            dgvAuthors.CellClick += dgvAuthors_CellClick;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(20, 370);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(75, 20);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "First Name:";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(110, 367);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(200, 27);
            txtFirstName.TabIndex = 3;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(330, 370);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(75, 20);
            lblLastName.TabIndex = 4;
            lblLastName.Text = "Last Name:";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(420, 367);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(200, 27);
            txtLastName.TabIndex = 5;
            // 
            // lblBio
            // 
            lblBio.AutoSize = true;
            lblBio.Location = new Point(20, 410);
            lblBio.Name = "lblBio";
            lblBio.Size = new Size(35, 20);
            lblBio.TabIndex = 6;
            lblBio.Text = "Bio:";
            // 
            // txtBio
            // 
            txtBio.Location = new Point(110, 407);
            txtBio.Multiline = true;
            txtBio.Name = "txtBio";
            txtBio.Size = new Size(510, 60);
            txtBio.TabIndex = 7;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(40, 167, 69);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(640, 365);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 38);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.Enabled = false;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(640, 410);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 38);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(640, 455);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(130, 38);
            btnClear.TabIndex = 10;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(0, 122, 204);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(640, 500);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(130, 38);
            btnBack.TabIndex = 13;
            btnBack.Text = "Back to Menu";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(20, 470);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(52, 20);
            lblSearch.TabIndex = 11;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(110, 467);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(510, 27);
            txtSearch.TabIndex = 12;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // AuthorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 550);
            Controls.Add(lblTitle);
            Controls.Add(dgvAuthors);
            Controls.Add(lblFirstName);
            Controls.Add(txtFirstName);
            Controls.Add(lblLastName);
            Controls.Add(txtLastName);
            Controls.Add(lblBio);
            Controls.Add(txtBio);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AuthorForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Author Management";
            ((System.ComponentModel.ISupportInitialize)dgvAuthors).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvAuthors;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblBio;
        private TextBox txtBio;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClear;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnBack;
    }
}
