namespace WinFormsApp1.Forms.Publisher
{
    partial class PublisherForm
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
            dgvPublishers = new DataGridView();
            lblName = new Label();
            txtName = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnBack = new Button();
            lblSearch = new Label();
            txtSearch = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvPublishers).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Publisher Management";
            // 
            // dgvPublishers
            // 
            dgvPublishers.AllowUserToAddRows = false;
            dgvPublishers.AllowUserToDeleteRows = false;
            dgvPublishers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPublishers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPublishers.Location = new Point(20, 50);
            dgvPublishers.MultiSelect = false;
            dgvPublishers.Name = "dgvPublishers";
            dgvPublishers.ReadOnly = true;
            dgvPublishers.RowHeadersWidth = 51;
            dgvPublishers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPublishers.Size = new Size(750, 300);
            dgvPublishers.TabIndex = 1;
            dgvPublishers.CellClick += dgvPublishers_CellClick;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 370);
            lblName.Name = "lblName";
            lblName.Size = new Size(51, 20);
            lblName.TabIndex = 2;
            lblName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Location = new Point(110, 367);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 3;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(20, 410);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(64, 20);
            lblAddress.TabIndex = 4;
            lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(110, 407);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(510, 27);
            txtAddress.TabIndex = 5;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(20, 450);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(53, 20);
            lblPhone.TabIndex = 6;
            lblPhone.Text = "Phone:";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(110, 447);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(200, 27);
            txtPhone.TabIndex = 7;
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
            lblSearch.Location = new Point(20, 490);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(52, 20);
            lblSearch.TabIndex = 11;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(110, 487);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(510, 27);
            txtSearch.TabIndex = 12;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // PublisherForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 550);
            Controls.Add(lblTitle);
            Controls.Add(dgvPublishers);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "PublisherForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Publisher Management";
            ((System.ComponentModel.ISupportInitialize)dgvPublishers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvPublishers;
        private Label lblName;
        private TextBox txtName;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblPhone;
        private TextBox txtPhone;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClear;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnBack;
    }
}
