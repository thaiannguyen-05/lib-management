namespace WinFormsApp1.Forms.Book
{
    partial class BookForm
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
            dgvBooks = new DataGridView();
            dgvCopies = new DataGridView();
            lblCopies = new Label();
            pnlForm = new Panel();
            lblTitleLbl = new Label();
            txtTitle = new TextBox();
            lblISBN = new Label();
            txtISBN = new TextBox();
            lblPublisher = new Label();
            cmbPublisher = new ComboBox();
            lblYear = new Label();
            nudPublicationYear = new NumericUpDown();
            lblCost = new Label();
            nudReplacementCost = new NumericUpDown();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblShelf = new Label();
            txtShelfLocation = new TextBox();
            lblAuthors = new Label();
            cmbAuthor = new ComboBox();
            lblCategories = new Label();
            clbCategories = new CheckedListBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            btnManageCopies = new Button();
            btnSearch = new Button();
            btnClear = new Button();
            btnBack = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCopies).BeginInit();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPublicationYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudReplacementCost).BeginInit();
            SuspendLayout();

            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(950, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Book Management";
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(20, 50);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(950, 250);
            dgvBooks.TabIndex = 1;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
            // 
            // dgvCopies
            // 
            dgvCopies.AllowUserToAddRows = false;
            dgvCopies.AllowUserToDeleteRows = false;
            dgvCopies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCopies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCopies.Location = new Point(20, 330);
            dgvCopies.MultiSelect = false;
            dgvCopies.Name = "dgvCopies";
            dgvCopies.ReadOnly = true;
            dgvCopies.RowHeadersWidth = 51;
            dgvCopies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCopies.Size = new Size(300, 180);
            dgvCopies.TabIndex = 2;
            // 
            // lblCopies
            // 
            lblCopies.AutoSize = true;
            lblCopies.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCopies.Location = new Point(20, 310);
            lblCopies.Name = "lblCopies";
            lblCopies.Size = new Size(100, 20);
            lblCopies.TabIndex = 3;
            lblCopies.Text = "Book Copies";
            // 
            // pnlForm
            // 
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            pnlForm.Controls.Add(lblTitleLbl);
            pnlForm.Controls.Add(txtTitle);
            pnlForm.Controls.Add(lblISBN);
            pnlForm.Controls.Add(txtISBN);
            pnlForm.Controls.Add(lblPublisher);
            pnlForm.Controls.Add(cmbPublisher);
            pnlForm.Controls.Add(lblYear);
            pnlForm.Controls.Add(nudPublicationYear);
            pnlForm.Controls.Add(lblCost);
            pnlForm.Controls.Add(nudReplacementCost);
            pnlForm.Controls.Add(lblDescription);
            pnlForm.Controls.Add(txtDescription);
            pnlForm.Controls.Add(lblShelf);
            pnlForm.Controls.Add(txtShelfLocation);
            pnlForm.Controls.Add(lblAuthors);
            pnlForm.Controls.Add(cmbAuthor);
            pnlForm.Controls.Add(lblCategories);
            pnlForm.Controls.Add(clbCategories);
            pnlForm.Enabled = false;
            pnlForm.Location = new Point(340, 310);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(680, 280);
            pnlForm.TabIndex = 4;
            // 
            // lblTitleLbl
            // 
            lblTitleLbl.AutoSize = true;
            lblTitleLbl.Location = new Point(10, 12);
            lblTitleLbl.Name = "lblTitleLbl";
            lblTitleLbl.Size = new Size(38, 20);
            lblTitleLbl.TabIndex = 0;
            lblTitleLbl.Text = "Title:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(80, 9);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(230, 27);
            txtTitle.TabIndex = 1;
            // 
            // lblISBN
            // 
            lblISBN.AutoSize = true;
            lblISBN.Location = new Point(10, 45);
            lblISBN.Name = "lblISBN";
            lblISBN.Size = new Size(42, 20);
            lblISBN.TabIndex = 2;
            lblISBN.Text = "ISBN:";
            // 
            // txtISBN
            // 
            txtISBN.Location = new Point(80, 42);
            txtISBN.Name = "txtISBN";
            txtISBN.Size = new Size(230, 27);
            txtISBN.TabIndex = 3;
            // 
            // lblPublisher
            // 
            lblPublisher.AutoSize = true;
            lblPublisher.Location = new Point(10, 78);
            lblPublisher.Name = "lblPublisher";
            lblPublisher.Size = new Size(72, 20);
            lblPublisher.TabIndex = 4;
            lblPublisher.Text = "Publisher:";
            // 
            // cmbPublisher
            // 
            cmbPublisher.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPublisher.Location = new Point(110, 75);
            cmbPublisher.Name = "cmbPublisher";
            cmbPublisher.Size = new Size(200, 28);
            cmbPublisher.TabIndex = 5;
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(10, 112);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(100, 20);
            lblYear.TabIndex = 6;
            lblYear.Text = "Pub. Year:";
            // 
            // nudPublicationYear
            // 
            nudPublicationYear.Location = new Point(110, 109);
            nudPublicationYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            nudPublicationYear.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPublicationYear.Name = "nudPublicationYear";
            nudPublicationYear.Size = new Size(100, 27);
            nudPublicationYear.TabIndex = 7;
            nudPublicationYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // lblCost
            // 
            lblCost.AutoSize = true;
            lblCost.Location = new Point(10, 145);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(110, 20);
            lblCost.TabIndex = 8;
            lblCost.Text = "Replace. Cost:";
            // 
            // nudReplacementCost
            // 
            nudReplacementCost.DecimalPlaces = 2;
            nudReplacementCost.Location = new Point(120, 142);
            nudReplacementCost.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudReplacementCost.Name = "nudReplacementCost";
            nudReplacementCost.Size = new Size(100, 27);
            nudReplacementCost.TabIndex = 9;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(10, 178);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(85, 20);
            lblDescription.TabIndex = 10;
            lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(110, 175);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 60);
            txtDescription.TabIndex = 11;
            // 
            // lblShelf
            // 
            lblShelf.AutoSize = true;
            lblShelf.Location = new Point(10, 248);
            lblShelf.Name = "lblShelf";
            lblShelf.Size = new Size(98, 20);
            lblShelf.TabIndex = 12;
            lblShelf.Text = "Shelf Location:";
            // 
            // txtShelfLocation
            // 
            txtShelfLocation.Location = new Point(110, 245);
            txtShelfLocation.Name = "txtShelfLocation";
            txtShelfLocation.Size = new Size(200, 27);
            txtShelfLocation.TabIndex = 13;
            // 
            // lblAuthors
            // 
            lblAuthors.AutoSize = true;
            lblAuthors.Location = new Point(330, 12);
            lblAuthors.Name = "lblAuthors";
            lblAuthors.Size = new Size(62, 20);
            lblAuthors.TabIndex = 14;
            lblAuthors.Text = "Authors:";
            // 
            // cmbAuthor
            // 
            cmbAuthor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuthor.Location = new Point(330, 35);
            cmbAuthor.Name = "cmbAuthor";
            cmbAuthor.Size = new Size(160, 28);
            cmbAuthor.TabIndex = 15;
            // 
            // lblCategories
            // 
            lblCategories.AutoSize = true;
            lblCategories.Location = new Point(500, 12);
            lblCategories.Name = "lblCategories";
            lblCategories.Size = new Size(79, 20);
            lblCategories.TabIndex = 16;
            lblCategories.Text = "Categories:";
            // 
            // clbCategories
            // 
            clbCategories.CheckOnClick = true;
            clbCategories.Location = new Point(500, 35);
            clbCategories.Name = "clbCategories";
            clbCategories.Size = new Size(160, 140);
            clbCategories.TabIndex = 17;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(40, 167, 69);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(340, 645);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 38);
            btnAdd.TabIndex = 5;
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
            btnEdit.Location = new Point(450, 645);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 38);
            btnEdit.TabIndex = 6;
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
            btnDelete.Location = new Point(560, 645);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 38);
            btnDelete.TabIndex = 7;
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
            btnSave.Location = new Point(670, 645);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 38);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.Enabled = false;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(780, 645);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 38);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnManageCopies
            // 
            btnManageCopies.BackColor = Color.FromArgb(253, 126, 20);
            btnManageCopies.Enabled = false;
            btnManageCopies.FlatStyle = FlatStyle.Flat;
            btnManageCopies.ForeColor = Color.White;
            btnManageCopies.Location = new Point(20, 520);
            btnManageCopies.Name = "btnManageCopies";
            btnManageCopies.Size = new Size(150, 38);
            btnManageCopies.TabIndex = 10;
            btnManageCopies.Text = "Manage Copies";
            btnManageCopies.UseVisualStyleBackColor = false;
            btnManageCopies.Click += btnManageCopies_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(20, 608);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(52, 20);
            lblSearch.TabIndex = 11;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(80, 605);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(340, 27);
            txtSearch.TabIndex = 12;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(0, 122, 204);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(430, 605);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 32);
            btnSearch.TabIndex = 13;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(520, 605);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(80, 32);
            btnClear.TabIndex = 14;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(890, 645);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 38);
            btnBack.TabIndex = 15;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // BookForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 700);
            Controls.Add(lblTitle);
            Controls.Add(dgvBooks);
            Controls.Add(lblCopies);
            Controls.Add(dgvCopies);
            Controls.Add(pnlForm);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(btnManageCopies);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(btnClear);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BookForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Book Management";
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCopies).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPublicationYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudReplacementCost).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvBooks;
        private DataGridView dgvCopies;
        private Label lblCopies;
        private Panel pnlForm;
        private Label lblTitleLbl;
        private TextBox txtTitle;
        private Label lblISBN;
        private TextBox txtISBN;
        private Label lblPublisher;
        private ComboBox cmbPublisher;
        private Label lblYear;
        private NumericUpDown nudPublicationYear;
        private Label lblCost;
        private NumericUpDown nudReplacementCost;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblShelf;
        private TextBox txtShelfLocation;
        private Label lblAuthors;
        private ComboBox cmbAuthor;
        private Label lblCategories;
        private CheckedListBox clbCategories;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
        private Button btnManageCopies;
        private Button btnSearch;
        private Button btnClear;
        private Button btnBack;
        private TextBox txtSearch;
        private Label lblSearch;
    }
}
