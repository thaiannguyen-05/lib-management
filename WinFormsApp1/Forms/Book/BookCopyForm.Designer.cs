namespace WinFormsApp1.Forms.Book
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
            lblDeck = new Label();
            cmbDeck = new ComboBox();
            lblRow = new Label();
            cmbRow = new ComboBox();
            lblFloor = new Label();
            cmbFloor = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnAddCopy = new Button();
            btnChangeStatus = new Button();
            btnDelete = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCopies).BeginInit();
            SuspendLayout();
            // 
            // lblBookTitle
            // 
            lblBookTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBookTitle.Location = new Point(20, 10);
            lblBookTitle.Name = "lblBookTitle";
            lblBookTitle.Size = new Size(550, 30);
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
            dgvCopies.Size = new Size(550, 250);
            dgvCopies.TabIndex = 1;
            dgvCopies.SelectionChanged += dgvCopies_SelectionChanged;
            // 
            // lblDeck
            // 
            lblDeck.AutoSize = true;
            lblDeck.Location = new Point(20, 320);
            lblDeck.Name = "lblDeck";
            lblDeck.Size = new Size(40, 20);
            lblDeck.TabIndex = 2;
            lblDeck.Text = "Deck:";
            // 
            // cmbDeck
            // 
            cmbDeck.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeck.Location = new Point(70, 317);
            cmbDeck.Name = "cmbDeck";
            cmbDeck.Size = new Size(60, 28);
            cmbDeck.TabIndex = 3;
            // 
            // lblRow
            // 
            lblRow.AutoSize = true;
            lblRow.Location = new Point(145, 320);
            lblRow.Name = "lblRow";
            lblRow.Size = new Size(38, 20);
            lblRow.TabIndex = 4;
            lblRow.Text = "Row:";
            // 
            // cmbRow
            // 
            cmbRow.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRow.Location = new Point(190, 317);
            cmbRow.Name = "cmbRow";
            cmbRow.Size = new Size(60, 28);
            cmbRow.TabIndex = 5;
            // 
            // lblFloor
            // 
            lblFloor.AutoSize = true;
            lblFloor.Location = new Point(265, 320);
            lblFloor.Name = "lblFloor";
            lblFloor.Size = new Size(42, 20);
            lblFloor.TabIndex = 6;
            lblFloor.Text = "Floor:";
            // 
            // cmbFloor
            // 
            cmbFloor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFloor.Location = new Point(315, 317);
            cmbFloor.Name = "cmbFloor";
            cmbFloor.Size = new Size(60, 28);
            cmbFloor.TabIndex = 7;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 360);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(70, 357);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(150, 28);
            cmbStatus.TabIndex = 9;
            // 
            // btnAddCopy
            // 
            btnAddCopy.BackColor = Color.FromArgb(40, 167, 69);
            btnAddCopy.FlatStyle = FlatStyle.Flat;
            btnAddCopy.ForeColor = Color.White;
            btnAddCopy.Location = new Point(310, 355);
            btnAddCopy.Name = "btnAddCopy";
            btnAddCopy.Size = new Size(120, 38);
            btnAddCopy.TabIndex = 10;
            btnAddCopy.Text = "Add Copy";
            btnAddCopy.UseVisualStyleBackColor = false;
            btnAddCopy.Click += btnAddCopy_Click;
            // 
            // btnChangeStatus
            // 
            btnChangeStatus.BackColor = Color.FromArgb(0, 122, 204);
            btnChangeStatus.Enabled = false;
            btnChangeStatus.FlatStyle = FlatStyle.Flat;
            btnChangeStatus.ForeColor = Color.White;
            btnChangeStatus.Location = new Point(440, 355);
            btnChangeStatus.Name = "btnChangeStatus";
            btnChangeStatus.Size = new Size(130, 38);
            btnChangeStatus.TabIndex = 11;
            btnChangeStatus.Text = "Change Status";
            btnChangeStatus.UseVisualStyleBackColor = false;
            btnChangeStatus.Click += btnChangeStatus_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.Enabled = false;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(310, 400);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 38);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(440, 400);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(130, 38);
            btnBack.TabIndex = 13;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // BookCopyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(590, 460);
            Controls.Add(lblBookTitle);
            Controls.Add(dgvCopies);
            Controls.Add(lblDeck);
            Controls.Add(cmbDeck);
            Controls.Add(lblRow);
            Controls.Add(cmbRow);
            Controls.Add(lblFloor);
            Controls.Add(cmbFloor);
            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);
            Controls.Add(btnAddCopy);
            Controls.Add(btnChangeStatus);
            Controls.Add(btnDelete);
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
        private Label lblDeck;
        private ComboBox cmbDeck;
        private Label lblRow;
        private ComboBox cmbRow;
        private Label lblFloor;
        private ComboBox cmbFloor;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Button btnAddCopy;
        private Button btnChangeStatus;
        private Button btnDelete;
        private Button btnBack;
    }
}
