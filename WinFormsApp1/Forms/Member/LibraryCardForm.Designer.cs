namespace WinFormsApp1.Forms
{
    partial class LibraryCardForm
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
            lblMember = new Label();
            cboMembers = new ComboBox();
            pnlCardInfo = new Panel();
            lblCardNumberLabel = new Label();
            lblCardNumber = new Label();
            lblExpiryDateLabel = new Label();
            lblExpiryDate = new Label();
            lblStatusLabel = new Label();
            lblStatus = new Label();
            lblWarning = new Label();
            btnIssue = new Button();
            btnRenew1M = new Button();
            btnRenew3M = new Button();
            btnRenew6M = new Button();
            btnRenew1Y = new Button();
            btnLockUnlock = new Button();
            btnBack = new Button();
            lblRenew = new Label();
            pnlCardInfo.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Library Card Management";
            // 
            // lblMember
            // 
            lblMember.AutoSize = true;
            lblMember.Location = new Point(20, 60);
            lblMember.Name = "lblMember";
            lblMember.Size = new Size(63, 20);
            lblMember.TabIndex = 1;
            lblMember.Text = "Member:";
            // 
            // cboMembers
            // 
            cboMembers.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMembers.Location = new Point(100, 57);
            cboMembers.Name = "cboMembers";
            cboMembers.Size = new Size(500, 28);
            cboMembers.TabIndex = 2;
            cboMembers.SelectedIndexChanged += cboMembers_SelectedIndexChanged;
            // 
            // pnlCardInfo
            // 
            pnlCardInfo.BorderStyle = BorderStyle.FixedSingle;
            pnlCardInfo.Controls.Add(lblCardNumberLabel);
            pnlCardInfo.Controls.Add(lblCardNumber);
            pnlCardInfo.Controls.Add(lblExpiryDateLabel);
            pnlCardInfo.Controls.Add(lblExpiryDate);
            pnlCardInfo.Controls.Add(lblStatusLabel);
            pnlCardInfo.Controls.Add(lblStatus);
            pnlCardInfo.Controls.Add(lblWarning);
            pnlCardInfo.Location = new Point(20, 100);
            pnlCardInfo.Name = "pnlCardInfo";
            pnlCardInfo.Size = new Size(580, 180);
            pnlCardInfo.TabIndex = 3;
            // 
            // lblCardNumberLabel
            // 
            lblCardNumberLabel.AutoSize = true;
            lblCardNumberLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCardNumberLabel.Location = new Point(15, 15);
            lblCardNumberLabel.Name = "lblCardNumberLabel";
            lblCardNumberLabel.Size = new Size(100, 20);
            lblCardNumberLabel.TabIndex = 0;
            lblCardNumberLabel.Text = "Card Number:";
            // 
            // lblCardNumber
            // 
            lblCardNumber.AutoSize = true;
            lblCardNumber.Location = new Point(150, 15);
            lblCardNumber.Name = "lblCardNumber";
            lblCardNumber.Size = new Size(0, 20);
            lblCardNumber.TabIndex = 1;
            // 
            // lblExpiryDateLabel
            // 
            lblExpiryDateLabel.AutoSize = true;
            lblExpiryDateLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblExpiryDateLabel.Location = new Point(15, 50);
            lblExpiryDateLabel.Name = "lblExpiryDateLabel";
            lblExpiryDateLabel.Size = new Size(90, 20);
            lblExpiryDateLabel.TabIndex = 2;
            lblExpiryDateLabel.Text = "Expiry Date:";
            // 
            // lblExpiryDate
            // 
            lblExpiryDate.AutoSize = true;
            lblExpiryDate.Location = new Point(150, 50);
            lblExpiryDate.Name = "lblExpiryDate";
            lblExpiryDate.Size = new Size(0, 20);
            lblExpiryDate.TabIndex = 3;
            // 
            // lblStatusLabel
            // 
            lblStatusLabel.AutoSize = true;
            lblStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusLabel.Location = new Point(15, 85);
            lblStatusLabel.Name = "lblStatusLabel";
            lblStatusLabel.Size = new Size(52, 20);
            lblStatusLabel.TabIndex = 4;
            lblStatusLabel.Text = "Status:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(150, 85);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 5;
            // 
            // lblWarning
            // 
            lblWarning.AutoSize = true;
            lblWarning.ForeColor = Color.FromArgb(255, 140, 0);
            lblWarning.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblWarning.Location = new Point(15, 130);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(0, 20);
            lblWarning.TabIndex = 6;
            lblWarning.Visible = false;
            // 
            // btnIssue
            // 
            btnIssue.BackColor = Color.FromArgb(40, 167, 69);
            btnIssue.FlatStyle = FlatStyle.Flat;
            btnIssue.ForeColor = Color.White;
            btnIssue.Location = new Point(20, 300);
            btnIssue.Name = "btnIssue";
            btnIssue.Size = new Size(130, 40);
            btnIssue.TabIndex = 4;
            btnIssue.Text = "Issue Card";
            btnIssue.UseVisualStyleBackColor = false;
            btnIssue.Click += btnIssue_Click;
            // 
            // lblRenew
            // 
            lblRenew.AutoSize = true;
            lblRenew.Location = new Point(170, 308);
            lblRenew.Name = "lblRenew";
            lblRenew.Size = new Size(52, 20);
            lblRenew.TabIndex = 5;
            lblRenew.Text = "Renew:";
            // 
            // btnRenew1M
            // 
            btnRenew1M.BackColor = Color.FromArgb(0, 122, 204);
            btnRenew1M.FlatStyle = FlatStyle.Flat;
            btnRenew1M.ForeColor = Color.White;
            btnRenew1M.Location = new Point(230, 300);
            btnRenew1M.Name = "btnRenew1M";
            btnRenew1M.Size = new Size(50, 40);
            btnRenew1M.TabIndex = 6;
            btnRenew1M.Text = "1M";
            btnRenew1M.UseVisualStyleBackColor = false;
            btnRenew1M.Click += btnRenew1M_Click;
            // 
            // btnRenew3M
            // 
            btnRenew3M.BackColor = Color.FromArgb(0, 122, 204);
            btnRenew3M.FlatStyle = FlatStyle.Flat;
            btnRenew3M.ForeColor = Color.White;
            btnRenew3M.Location = new Point(290, 300);
            btnRenew3M.Name = "btnRenew3M";
            btnRenew3M.Size = new Size(50, 40);
            btnRenew3M.TabIndex = 7;
            btnRenew3M.Text = "3M";
            btnRenew3M.UseVisualStyleBackColor = false;
            btnRenew3M.Click += btnRenew3M_Click;
            // 
            // btnRenew6M
            // 
            btnRenew6M.BackColor = Color.FromArgb(0, 122, 204);
            btnRenew6M.FlatStyle = FlatStyle.Flat;
            btnRenew6M.ForeColor = Color.White;
            btnRenew6M.Location = new Point(350, 300);
            btnRenew6M.Name = "btnRenew6M";
            btnRenew6M.Size = new Size(50, 40);
            btnRenew6M.TabIndex = 8;
            btnRenew6M.Text = "6M";
            btnRenew6M.UseVisualStyleBackColor = false;
            btnRenew6M.Click += btnRenew6M_Click;
            // 
            // btnRenew1Y
            // 
            btnRenew1Y.BackColor = Color.FromArgb(0, 122, 204);
            btnRenew1Y.FlatStyle = FlatStyle.Flat;
            btnRenew1Y.ForeColor = Color.White;
            btnRenew1Y.Location = new Point(410, 300);
            btnRenew1Y.Name = "btnRenew1Y";
            btnRenew1Y.Size = new Size(50, 40);
            btnRenew1Y.TabIndex = 9;
            btnRenew1Y.Text = "1Y";
            btnRenew1Y.UseVisualStyleBackColor = false;
            btnRenew1Y.Click += btnRenew1Y_Click;
            // 
            // btnLockUnlock
            // 
            btnLockUnlock.BackColor = Color.FromArgb(220, 53, 69);
            btnLockUnlock.FlatStyle = FlatStyle.Flat;
            btnLockUnlock.ForeColor = Color.White;
            btnLockUnlock.Location = new Point(470, 300);
            btnLockUnlock.Name = "btnLockUnlock";
            btnLockUnlock.Size = new Size(130, 40);
            btnLockUnlock.TabIndex = 10;
            btnLockUnlock.Text = "Lock Card";
            btnLockUnlock.UseVisualStyleBackColor = false;
            btnLockUnlock.Click += btnLockUnlock_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(20, 360);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(130, 40);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back to Menu";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // LibraryCardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 420);
            Controls.Add(lblTitle);
            Controls.Add(lblMember);
            Controls.Add(cboMembers);
            Controls.Add(pnlCardInfo);
            Controls.Add(btnIssue);
            Controls.Add(lblRenew);
            Controls.Add(btnRenew1M);
            Controls.Add(btnRenew3M);
            Controls.Add(btnRenew6M);
            Controls.Add(btnRenew1Y);
            Controls.Add(btnLockUnlock);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LibraryCardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Library Card Management";
            pnlCardInfo.ResumeLayout(false);
            pnlCardInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblMember;
        private ComboBox cboMembers;
        private Panel pnlCardInfo;
        private Label lblCardNumberLabel;
        private Label lblCardNumber;
        private Label lblExpiryDateLabel;
        private Label lblExpiryDate;
        private Label lblStatusLabel;
        private Label lblStatus;
        private Label lblWarning;
        private Button btnIssue;
        private Label lblRenew;
        private Button btnRenew1M;
        private Button btnRenew3M;
        private Button btnRenew6M;
        private Button btnRenew1Y;
        private Button btnLockUnlock;
        private Button btnBack;
    }
}
