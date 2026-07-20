namespace WinFormsApp1.Forms.Borrow
{
    partial class BorrowForm
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
            grpMember = new GroupBox();
            lblMemberSelect = new Label();
            cmbMembers = new ComboBox();
            lblMemberName = new Label();
            lblMemberEmail = new Label();
            lblMemberCard = new Label();
            lblMemberBorrows = new Label();
            grpBook = new GroupBox();
            lblBookSearch = new Label();
            txtBookSearch = new TextBox();
            btnSearchBook = new Button();
            lblBookTitle = new Label();
            lblBookBarcode = new Label();
            lblBookAuthors = new Label();
            lblBookShelf = new Label();
            btnBorrow = new Button();
            lblError = new Label();
            dgvActiveBorrows = new DataGridView();
            lblActiveBorrows = new Label();
            btnBack = new Button();
            grpMember.SuspendLayout();
            grpBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActiveBorrows).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Borrow Book";
            // 
            // grpMember
            // 
            grpMember.Controls.Add(lblMemberSelect);
            grpMember.Controls.Add(cmbMembers);
            grpMember.Controls.Add(lblMemberName);
            grpMember.Controls.Add(lblMemberEmail);
            grpMember.Controls.Add(lblMemberCard);
            grpMember.Controls.Add(lblMemberBorrows);
            grpMember.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpMember.Location = new Point(20, 55);
            grpMember.Name = "grpMember";
            grpMember.Size = new Size(750, 130);
            grpMember.TabIndex = 1;
            grpMember.TabStop = false;
            grpMember.Text = "Member Information";
            // 
            // lblMemberSelect
            // 
            lblMemberSelect.AutoSize = true;
            lblMemberSelect.Font = new Font("Segoe UI", 9F);
            lblMemberSelect.Location = new Point(15, 30);
            lblMemberSelect.Name = "lblMemberSelect";
            lblMemberSelect.Size = new Size(100, 20);
            lblMemberSelect.TabIndex = 0;
            lblMemberSelect.Text = "Select Member:";
            // 
            // cmbMembers
            // 
            cmbMembers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMembers.Font = new Font("Segoe UI", 9F);
            cmbMembers.Location = new Point(125, 27);
            cmbMembers.Name = "cmbMembers";
            cmbMembers.Size = new Size(450, 28);
            cmbMembers.TabIndex = 1;
            cmbMembers.SelectedIndexChanged += cmbMembers_SelectedIndexChanged;
            // 
            // lblMemberName
            // 
            lblMemberName.AutoSize = true;
            lblMemberName.Font = new Font("Segoe UI", 9F);
            lblMemberName.Location = new Point(15, 65);
            lblMemberName.Name = "lblMemberName";
            lblMemberName.Size = new Size(52, 20);
            lblMemberName.TabIndex = 2;
            lblMemberName.Text = "Name: -";
            // 
            // lblMemberEmail
            // 
            lblMemberEmail.AutoSize = true;
            lblMemberEmail.Font = new Font("Segoe UI", 9F);
            lblMemberEmail.Location = new Point(15, 90);
            lblMemberEmail.Name = "lblMemberEmail";
            lblMemberEmail.Size = new Size(55, 20);
            lblMemberEmail.TabIndex = 3;
            lblMemberEmail.Text = "Email: -";
            // 
            // lblMemberCard
            // 
            lblMemberCard.AutoSize = true;
            lblMemberCard.Font = new Font("Segoe UI", 9F);
            lblMemberCard.Location = new Point(400, 65);
            lblMemberCard.Name = "lblMemberCard";
            lblMemberCard.Size = new Size(85, 20);
            lblMemberCard.TabIndex = 4;
            lblMemberCard.Text = "Card: -";
            // 
            // lblMemberBorrows
            // 
            lblMemberBorrows.AutoSize = true;
            lblMemberBorrows.Font = new Font("Segoe UI", 9F);
            lblMemberBorrows.Location = new Point(400, 90);
            lblMemberBorrows.Name = "lblMemberBorrows";
            lblMemberBorrows.Size = new Size(110, 20);
            lblMemberBorrows.TabIndex = 5;
            lblMemberBorrows.Text = "Active Borrows: -";
            // 
            // grpBook
            // 
            grpBook.Controls.Add(lblBookSearch);
            grpBook.Controls.Add(txtBookSearch);
            grpBook.Controls.Add(btnSearchBook);
            grpBook.Controls.Add(lblBookTitle);
            grpBook.Controls.Add(lblBookBarcode);
            grpBook.Controls.Add(lblBookAuthors);
            grpBook.Controls.Add(lblBookShelf);
            grpBook.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpBook.Location = new Point(20, 195);
            grpBook.Name = "grpBook";
            grpBook.Size = new Size(750, 130);
            grpBook.TabIndex = 2;
            grpBook.TabStop = false;
            grpBook.Text = "Book Information";
            // 
            // lblBookSearch
            // 
            lblBookSearch.AutoSize = true;
            lblBookSearch.Font = new Font("Segoe UI", 9F);
            lblBookSearch.Location = new Point(15, 30);
            lblBookSearch.Name = "lblBookSearch";
            lblBookSearch.Size = new Size(135, 20);
            lblBookSearch.TabIndex = 0;
            lblBookSearch.Text = "Enter Barcode/ISBN:";
            // 
            // txtBookSearch
            // 
            txtBookSearch.Font = new Font("Segoe UI", 9F);
            txtBookSearch.Location = new Point(158, 27);
            txtBookSearch.Name = "txtBookSearch";
            txtBookSearch.Size = new Size(337, 27);
            txtBookSearch.TabIndex = 1;
            // 
            // btnSearchBook
            // 
            btnSearchBook.BackColor = Color.FromArgb(0, 122, 204);
            btnSearchBook.FlatStyle = FlatStyle.Flat;
            btnSearchBook.Font = new Font("Segoe UI", 9F);
            btnSearchBook.ForeColor = Color.White;
            btnSearchBook.Location = new Point(505, 25);
            btnSearchBook.Name = "btnSearchBook";
            btnSearchBook.Size = new Size(80, 30);
            btnSearchBook.TabIndex = 2;
            btnSearchBook.Text = "Search";
            btnSearchBook.UseVisualStyleBackColor = false;
            btnSearchBook.Click += btnSearchBook_Click;
            // 
            // lblBookTitle
            // 
            lblBookTitle.AutoSize = true;
            lblBookTitle.Font = new Font("Segoe UI", 9F);
            lblBookTitle.Location = new Point(15, 65);
            lblBookTitle.Name = "lblBookTitle";
            lblBookTitle.Size = new Size(46, 20);
            lblBookTitle.TabIndex = 3;
            lblBookTitle.Text = "Title: -";
            // 
            // lblBookBarcode
            // 
            lblBookBarcode.AutoSize = true;
            lblBookBarcode.Font = new Font("Segoe UI", 9F);
            lblBookBarcode.Location = new Point(15, 90);
            lblBookBarcode.Name = "lblBookBarcode";
            lblBookBarcode.Size = new Size(75, 20);
            lblBookBarcode.TabIndex = 4;
            lblBookBarcode.Text = "Barcode: -";
            // 
            // lblBookAuthors
            // 
            lblBookAuthors.AutoSize = true;
            lblBookAuthors.Font = new Font("Segoe UI", 9F);
            lblBookAuthors.Location = new Point(400, 65);
            lblBookAuthors.Name = "lblBookAuthors";
            lblBookAuthors.Size = new Size(65, 20);
            lblBookAuthors.TabIndex = 5;
            lblBookAuthors.Text = "Authors: -";
            // 
            // lblBookShelf
            // 
            lblBookShelf.AutoSize = true;
            lblBookShelf.Font = new Font("Segoe UI", 9F);
            lblBookShelf.Location = new Point(400, 90);
            lblBookShelf.Name = "lblBookShelf";
            lblBookShelf.Size = new Size(55, 20);
            lblBookShelf.TabIndex = 6;
            lblBookShelf.Text = "Shelf: -";
            // 
            // btnBorrow
            // 
            btnBorrow.BackColor = Color.FromArgb(40, 167, 69);
            btnBorrow.Enabled = false;
            btnBorrow.FlatStyle = FlatStyle.Flat;
            btnBorrow.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBorrow.ForeColor = Color.White;
            btnBorrow.Location = new Point(20, 335);
            btnBorrow.Name = "btnBorrow";
            btnBorrow.Size = new Size(150, 40);
            btnBorrow.TabIndex = 3;
            btnBorrow.Text = "Borrow Book";
            btnBorrow.UseVisualStyleBackColor = false;
            btnBorrow.Click += btnBorrow_Click;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.Red;
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.Location = new Point(180, 335);
            lblError.Name = "lblError";
            lblError.Size = new Size(590, 40);
            lblError.TabIndex = 4;
            lblError.Text = "";
            lblError.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblActiveBorrows
            // 
            lblActiveBorrows.AutoSize = true;
            lblActiveBorrows.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblActiveBorrows.Location = new Point(20, 390);
            lblActiveBorrows.Name = "lblActiveBorrows";
            lblActiveBorrows.Size = new Size(200, 20);
            lblActiveBorrows.TabIndex = 5;
            lblActiveBorrows.Text = "Active Borrows of Member:";
            // 
            // dgvActiveBorrows
            // 
            dgvActiveBorrows.AllowUserToAddRows = false;
            dgvActiveBorrows.AllowUserToDeleteRows = false;
            dgvActiveBorrows.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActiveBorrows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActiveBorrows.Location = new Point(20, 415);
            dgvActiveBorrows.MultiSelect = false;
            dgvActiveBorrows.Name = "dgvActiveBorrows";
            dgvActiveBorrows.ReadOnly = true;
            dgvActiveBorrows.RowHeadersWidth = 51;
            dgvActiveBorrows.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActiveBorrows.Size = new Size(750, 180);
            dgvActiveBorrows.TabIndex = 6;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9F);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(20, 605);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 35);
            btnBack.TabIndex = 7;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // BorrowForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 655);
            Controls.Add(lblTitle);
            Controls.Add(grpMember);
            Controls.Add(grpBook);
            Controls.Add(btnBorrow);
            Controls.Add(lblError);
            Controls.Add(lblActiveBorrows);
            Controls.Add(dgvActiveBorrows);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BorrowForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Borrow Book";
            grpMember.ResumeLayout(false);
            grpMember.PerformLayout();
            grpBook.ResumeLayout(false);
            grpBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActiveBorrows).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox grpMember;
        private Label lblMemberSelect;
        private ComboBox cmbMembers;
        private Label lblMemberName;
        private Label lblMemberEmail;
        private Label lblMemberCard;
        private Label lblMemberBorrows;
        private GroupBox grpBook;
        private Label lblBookSearch;
        private TextBox txtBookSearch;
        private Button btnSearchBook;
        private Label lblBookTitle;
        private Label lblBookBarcode;
        private Label lblBookAuthors;
        private Label lblBookShelf;
        private Button btnBorrow;
        private Label lblError;
        private Label lblActiveBorrows;
        private DataGridView dgvActiveBorrows;
        private Button btnBack;
    }
}
