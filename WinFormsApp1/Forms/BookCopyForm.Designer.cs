namespace WinFormsApp1.Forms
{
    partial class BookCopyForm
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
            lblBookTitle = new Label();
            dgvCopies = new DataGridView();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCopies).BeginInit();
            SuspendLayout();

            // 
            // lblBookTitle
            // 
            lblBookTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBookTitle.Location = new Point(20, 10);
            lblBookTitle.Name = "lblBookTitle";
            lblBookTitle.Size = new Size(500, 30);
            lblBookTitle.TabIndex = 0;
            lblBookTitle.Text = "Book Copies";
            // 
            // dgvCopies
            // 
            dgvCopies.AllowUserToAddRows = false;
            dgvCopies.AllowUserToDeleteRows = false;
            dgvCopies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCopies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCopies.Location = new Point(20, 50);
            dgvCopies.MultiSelect = false;
            dgvCopies.Name = "dgvCopies";
            dgvCopies.ReadOnly = true;
            dgvCopies.RowHeadersWidth = 51;
            dgvCopies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCopies.Size = new Size(300, 200);
            dgvCopies.TabIndex = 1;
            dgvCopies.SelectionChanged += dgvCopies_SelectionChanged;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 270);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(80, 267);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(150, 28);
            cmbStatus.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(40, 167, 69);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(340, 50);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 38);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add New Copy";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(0, 122, 204);
            btnEdit.Enabled = false;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(340, 100);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(130, 38);
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
            btnDelete.Location = new Point(340, 150);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 38);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.Enabled = false;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(340, 200);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(130, 38);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save Changes";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(340, 250);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 38);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(340, 300);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(130, 38);
            btnBack.TabIndex = 9;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // BookCopyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 360);
            Controls.Add(lblBookTitle);
            Controls.Add(dgvCopies);
            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BookCopyForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manage Book Copies";
            ((System.ComponentModel.ISupportInitialize)dgvCopies).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBookTitle;
        private DataGridView dgvCopies;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
        private Button btnBack;
    }
}
