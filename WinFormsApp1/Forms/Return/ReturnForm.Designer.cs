namespace WinFormsApp1.Forms.Return
{
    partial class ReturnForm
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
            lblCondition = new Label();
            cmbCondition = new ComboBox();
            dgvBorrows = new DataGridView();
            lblFeeInfo = new Label();
            btnReturn = new Button();
            btnReturnMultiple = new Button();
            btnLoadOverdue = new Button();
            lblError = new Label();
            btnBack = new Button();
            grpMember.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBorrows).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Return Book";
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
            lblMemberSelect.Size = new Size(180, 20);
            lblMemberSelect.TabIndex = 0;
            lblMemberSelect.Text = "Search Member (name/email):";
            // 
            // cmbMembers
            // 
            cmbMembers.DropDownStyle = ComboBoxStyle.DropDown;
            cmbMembers.Font = new Font("Segoe UI", 9F);
            cmbMembers.Location = new Point(205, 27);
            cmbMembers.Name = "cmbMembers";
            cmbMembers.Size = new Size(400, 28);
            cmbMembers.TabIndex = 1;
            cmbMembers.TextUpdate += cmbMembers_TextUpdate;
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
            // lblCondition
            // 
            lblCondition.AutoSize = true;
            lblCondition.Font = new Font("Segoe UI", 9F);
            lblCondition.Location = new Point(20, 200);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new Size(120, 20);
            lblCondition.TabIndex = 2;
            lblCondition.Text = "Book Condition:";
            // 
            // cmbCondition
            // 
            cmbCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCondition.Font = new Font("Segoe UI", 9F);
            cmbCondition.Items.AddRange(new object[] { "Bình thường", "Hỏng", "Mất" });
            cmbCondition.Location = new Point(150, 197);
            cmbCondition.Name = "cmbCondition";
            cmbCondition.Size = new Size(200, 28);
            cmbCondition.TabIndex = 3;
            // 
            // btnLoadOverdue
            // 
            btnLoadOverdue.BackColor = Color.FromArgb(220, 53, 69);
            btnLoadOverdue.FlatStyle = FlatStyle.Flat;
            btnLoadOverdue.Font = new Font("Segoe UI", 9F);
            btnLoadOverdue.ForeColor = Color.White;
            btnLoadOverdue.Location = new Point(570, 197);
            btnLoadOverdue.Name = "btnLoadOverdue";
            btnLoadOverdue.Size = new Size(200, 30);
            btnLoadOverdue.TabIndex = 4;
            btnLoadOverdue.Text = "Show All Overdue";
            btnLoadOverdue.UseVisualStyleBackColor = false;
            btnLoadOverdue.Click += btnLoadOverdue_Click;
            // 
            // dgvBorrows
            // 
            dgvBorrows.AllowUserToAddRows = false;
            dgvBorrows.AllowUserToDeleteRows = false;
            dgvBorrows.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBorrows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBorrows.Location = new Point(20, 235);
            dgvBorrows.Name = "dgvBorrows";
            dgvBorrows.ReadOnly = true;
            dgvBorrows.RowHeadersWidth = 51;
            dgvBorrows.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBorrows.Size = new Size(750, 230);
            dgvBorrows.TabIndex = 5;
            dgvBorrows.MultiSelect = true;
            dgvBorrows.SelectionChanged += dgvBorrows_SelectionChanged;
            // 
            // lblFeeInfo
            // 
            lblFeeInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFeeInfo.ForeColor = Color.Red;
            lblFeeInfo.Location = new Point(20, 475);
            lblFeeInfo.Name = "lblFeeInfo";
            lblFeeInfo.Size = new Size(750, 25);
            lblFeeInfo.TabIndex = 6;
            lblFeeInfo.Text = "";
            // 
            // btnReturn
            // 
            btnReturn.BackColor = Color.FromArgb(40, 167, 69);
            btnReturn.Enabled = false;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReturn.ForeColor = Color.White;
            btnReturn.Location = new Point(20, 510);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(150, 40);
            btnReturn.TabIndex = 7;
            btnReturn.Text = "Return Book";
            btnReturn.UseVisualStyleBackColor = false;
            btnReturn.Click += btnReturn_Click;
            // 
            // btnReturnMultiple
            // 
            btnReturnMultiple.BackColor = Color.FromArgb(255, 193, 7);
            btnReturnMultiple.Enabled = false;
            btnReturnMultiple.FlatStyle = FlatStyle.Flat;
            btnReturnMultiple.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReturnMultiple.ForeColor = Color.Black;
            btnReturnMultiple.Location = new Point(185, 510);
            btnReturnMultiple.Name = "btnReturnMultiple";
            btnReturnMultiple.Size = new Size(180, 40);
            btnReturnMultiple.TabIndex = 8;
            btnReturnMultiple.Text = "Return Selected";
            btnReturnMultiple.UseVisualStyleBackColor = false;
            btnReturnMultiple.Click += btnReturnMultiple_Click;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.Red;
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.Location = new Point(380, 510);
            lblError.Name = "lblError";
            lblError.Size = new Size(390, 40);
            lblError.TabIndex = 9;
            lblError.Text = "";
            lblError.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9F);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(20, 565);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 35);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // ReturnForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 615);
            Controls.Add(lblTitle);
            Controls.Add(grpMember);
            Controls.Add(lblCondition);
            Controls.Add(cmbCondition);
            Controls.Add(btnLoadOverdue);
            Controls.Add(dgvBorrows);
            Controls.Add(lblFeeInfo);
            Controls.Add(btnReturn);
            Controls.Add(btnReturnMultiple);
            Controls.Add(lblError);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ReturnForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Return Book";
            grpMember.ResumeLayout(false);
            grpMember.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBorrows).EndInit();
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
        private Label lblCondition;
        private ComboBox cmbCondition;
        private DataGridView dgvBorrows;
        private Label lblFeeInfo;
        private Button btnReturn;
        private Button btnReturnMultiple;
        private Button btnLoadOverdue;
        private Label lblError;
        private Button btnBack;
    }
}
