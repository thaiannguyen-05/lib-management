namespace WinFormsApp1.Forms.Borrow
{
    partial class RenewForm
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
            dgvActiveBorrows = new DataGridView();
            lblActiveBorrows = new Label();
            grpRenewal = new GroupBox();
            lblRenewBookTitle = new Label();
            lblRenewBorrowDate = new Label();
            lblRenewDueDate = new Label();
            lblRenewedCount = new Label();
            lblRemainingRenewals = new Label();
            btnRenew = new Button();
            lblRenewalError = new Label();
            btnBack = new Button();
            grpMember.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActiveBorrows).BeginInit();
            grpRenewal.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(760, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Renew Book";
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
            grpMember.Size = new Size(760, 130);
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
            cmbMembers.Size = new Size(460, 28);
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
            lblMemberCard.Location = new Point(410, 65);
            lblMemberCard.Name = "lblMemberCard";
            lblMemberCard.Size = new Size(43, 20);
            lblMemberCard.TabIndex = 4;
            lblMemberCard.Text = "Card: -";
            // 
            // lblMemberBorrows
            // 
            lblMemberBorrows.AutoSize = true;
            lblMemberBorrows.Font = new Font("Segoe UI", 9F);
            lblMemberBorrows.Location = new Point(410, 90);
            lblMemberBorrows.Name = "lblMemberBorrows";
            lblMemberBorrows.Size = new Size(110, 20);
            lblMemberBorrows.TabIndex = 5;
            lblMemberBorrows.Text = "Active Borrows: -";
            // 
            // lblActiveBorrows
            // 
            lblActiveBorrows.AutoSize = true;
            lblActiveBorrows.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblActiveBorrows.Location = new Point(20, 200);
            lblActiveBorrows.Name = "lblActiveBorrows";
            lblActiveBorrows.Size = new Size(199, 20);
            lblActiveBorrows.TabIndex = 2;
            lblActiveBorrows.Text = "Select borrow to renew:";
            // 
            // dgvActiveBorrows
            // 
            dgvActiveBorrows.AllowUserToAddRows = false;
            dgvActiveBorrows.AllowUserToDeleteRows = false;
            dgvActiveBorrows.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActiveBorrows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActiveBorrows.Location = new Point(20, 225);
            dgvActiveBorrows.MultiSelect = false;
            dgvActiveBorrows.Name = "dgvActiveBorrows";
            dgvActiveBorrows.ReadOnly = true;
            dgvActiveBorrows.RowHeadersWidth = 51;
            dgvActiveBorrows.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActiveBorrows.Size = new Size(760, 180);
            dgvActiveBorrows.TabIndex = 3;
            dgvActiveBorrows.SelectionChanged += dgvActiveBorrows_SelectionChanged;
            // 
            // grpRenewal
            // 
            grpRenewal.Controls.Add(lblRenewBookTitle);
            grpRenewal.Controls.Add(lblRenewBorrowDate);
            grpRenewal.Controls.Add(lblRenewDueDate);
            grpRenewal.Controls.Add(lblRenewedCount);
            grpRenewal.Controls.Add(lblRemainingRenewals);
            grpRenewal.Controls.Add(btnRenew);
            grpRenewal.Controls.Add(lblRenewalError);
            grpRenewal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpRenewal.Location = new Point(20, 415);
            grpRenewal.Name = "grpRenewal";
            grpRenewal.Size = new Size(760, 110);
            grpRenewal.TabIndex = 4;
            grpRenewal.TabStop = false;
            grpRenewal.Text = "Renewal Details";
            // 
            // lblRenewBookTitle
            // 
            lblRenewBookTitle.AutoSize = true;
            lblRenewBookTitle.Font = new Font("Segoe UI", 9F);
            lblRenewBookTitle.Location = new Point(15, 28);
            lblRenewBookTitle.Name = "lblRenewBookTitle";
            lblRenewBookTitle.Size = new Size(46, 20);
            lblRenewBookTitle.TabIndex = 0;
            lblRenewBookTitle.Text = "Book: -";
            // 
            // lblRenewBorrowDate
            // 
            lblRenewBorrowDate.AutoSize = true;
            lblRenewBorrowDate.Font = new Font("Segoe UI", 9F);
            lblRenewBorrowDate.Location = new Point(15, 50);
            lblRenewBorrowDate.Name = "lblRenewBorrowDate";
            lblRenewBorrowDate.Size = new Size(80, 20);
            lblRenewBorrowDate.TabIndex = 1;
            lblRenewBorrowDate.Text = "Borrowed: -";
            // 
            // lblRenewDueDate
            // 
            lblRenewDueDate.AutoSize = true;
            lblRenewDueDate.Font = new Font("Segoe UI", 9F);
            lblRenewDueDate.Location = new Point(200, 28);
            lblRenewDueDate.Name = "lblRenewDueDate";
            lblRenewDueDate.Size = new Size(78, 20);
            lblRenewDueDate.TabIndex = 2;
            lblRenewDueDate.Text = "Due date: -";
            // 
            // lblRenewedCount
            // 
            lblRenewedCount.AutoSize = true;
            lblRenewedCount.Font = new Font("Segoe UI", 9F);
            lblRenewedCount.Location = new Point(200, 50);
            lblRenewedCount.Name = "lblRenewedCount";
            lblRenewedCount.Size = new Size(80, 20);
            lblRenewedCount.TabIndex = 3;
            lblRenewedCount.Text = "Renewed: -";
            // 
            // lblRemainingRenewals
            // 
            lblRemainingRenewals.AutoSize = true;
            lblRemainingRenewals.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRemainingRenewals.Location = new Point(390, 28);
            lblRemainingRenewals.Name = "lblRemainingRenewals";
            lblRemainingRenewals.Size = new Size(155, 20);
            lblRemainingRenewals.TabIndex = 4;
            lblRemainingRenewals.Text = "Remaining renewals: -";
            // 
            // btnRenew
            // 
            btnRenew.BackColor = Color.FromArgb(255, 193, 7);
            btnRenew.Enabled = false;
            btnRenew.FlatStyle = FlatStyle.Flat;
            btnRenew.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRenew.ForeColor = Color.Black;
            btnRenew.Location = new Point(610, 24);
            btnRenew.Name = "btnRenew";
            btnRenew.Size = new Size(130, 34);
            btnRenew.TabIndex = 5;
            btnRenew.Text = "Gia hạn";
            btnRenew.UseVisualStyleBackColor = false;
            btnRenew.Click += btnRenew_Click;
            // 
            // lblRenewalError
            // 
            lblRenewalError.ForeColor = Color.Red;
            lblRenewalError.Font = new Font("Segoe UI", 9F);
            lblRenewalError.Location = new Point(15, 74);
            lblRenewalError.Name = "lblRenewalError";
            lblRenewalError.Size = new Size(725, 25);
            lblRenewalError.TabIndex = 6;
            lblRenewalError.Text = "";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9F);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(20, 540);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 35);
            btnBack.TabIndex = 5;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // RenewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 590);
            Controls.Add(lblTitle);
            Controls.Add(grpMember);
            Controls.Add(lblActiveBorrows);
            Controls.Add(dgvActiveBorrows);
            Controls.Add(grpRenewal);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "RenewForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Renew Book";
            grpMember.ResumeLayout(false);
            grpMember.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActiveBorrows).EndInit();
            grpRenewal.ResumeLayout(false);
            grpRenewal.PerformLayout();
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
        private Label lblActiveBorrows;
        private DataGridView dgvActiveBorrows;
        private GroupBox grpRenewal;
        private Label lblRenewBookTitle;
        private Label lblRenewBorrowDate;
        private Label lblRenewDueDate;
        private Label lblRenewedCount;
        private Label lblRemainingRenewals;
        private Button btnRenew;
        private Label lblRenewalError;
        private Button btnBack;
    }
}
