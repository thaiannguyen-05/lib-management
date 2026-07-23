namespace WinFormsApp1.Forms
{
    partial class FeeForm
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
            dgvFees = new DataGridView();
            lblAmount = new Label();
            nudAmount = new NumericUpDown();
            btnPay = new Button();
            btnWaive = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            lblTotalDebt = new Label();
            lblTotalDebtValue = new Label();
            lblStatus = new Label();
            lblAdminNote = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFees).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAmount).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 22);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1350, 48);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quản lý phí thành viên";
            // 
            // lblMember
            // 
            lblMember.AutoSize = true;
            lblMember.Location = new Point(30, 90);
            lblMember.Margin = new Padding(4, 0, 4, 0);
            lblMember.Name = "lblMember";
            lblMember.Size = new Size(115, 30);
            lblMember.TabIndex = 1;
            lblMember.Text = "Thành viên";
            // 
            // cboMembers
            // 
            cboMembers.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMembers.Location = new Point(165, 86);
            cboMembers.Margin = new Padding(4, 4, 4, 4);
            cboMembers.Name = "cboMembers";
            cboMembers.Size = new Size(778, 38);
            cboMembers.TabIndex = 2;
            cboMembers.SelectedIndexChanged += cboMembers_SelectedIndexChanged;
            // 
            // dgvFees
            // 
            dgvFees.AllowUserToAddRows = false;
            dgvFees.AllowUserToDeleteRows = false;
            dgvFees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFees.Location = new Point(30, 150);
            dgvFees.Margin = new Padding(4, 4, 4, 4);
            dgvFees.MultiSelect = false;
            dgvFees.Name = "dgvFees";
            dgvFees.ReadOnly = true;
            dgvFees.RowHeadersWidth = 51;
            dgvFees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFees.Size = new Size(1350, 480);
            dgvFees.TabIndex = 3;
            dgvFees.CellClick += dgvFees_CellClick;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(30, 660);
            lblAmount.Margin = new Padding(4, 0, 4, 0);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(185, 30);
            lblAmount.TabIndex = 4;
            lblAmount.Text = "Số tiền thanh toán";
            // 
            // nudAmount
            // 
            nudAmount.Location = new Point(240, 657);
            nudAmount.Margin = new Padding(4, 4, 4, 4);
            nudAmount.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            nudAmount.Name = "nudAmount";
            nudAmount.Size = new Size(270, 35);
            nudAmount.TabIndex = 5;
            // 
            // btnPay
            // 
            btnPay.BackColor = Color.FromArgb(40, 167, 69);
            btnPay.FlatStyle = FlatStyle.Flat;
            btnPay.ForeColor = Color.White;
            btnPay.Location = new Point(540, 648);
            btnPay.Margin = new Padding(4, 4, 4, 4);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(180, 57);
            btnPay.TabIndex = 6;
            btnPay.Text = "Thanh toán";
            btnPay.UseVisualStyleBackColor = false;
            btnPay.Click += btnPay_Click;
            // 
            // btnWaive
            // 
            btnWaive.BackColor = Color.FromArgb(220, 53, 69);
            btnWaive.FlatStyle = FlatStyle.Flat;
            btnWaive.ForeColor = Color.White;
            btnWaive.Location = new Point(735, 648);
            btnWaive.Margin = new Padding(4, 4, 4, 4);
            btnWaive.Name = "btnWaive";
            btnWaive.Size = new Size(180, 57);
            btnWaive.TabIndex = 7;
            btnWaive.Text = "Miễn phạt";
            btnWaive.UseVisualStyleBackColor = false;
            btnWaive.Click += btnWaive_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(0, 122, 204);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(930, 648);
            btnRefresh.Margin = new Padding(4, 4, 4, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(180, 57);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Tải lại";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(1200, 648);
            btnBack.Margin = new Padding(4, 4, 4, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(180, 57);
            btnBack.TabIndex = 9;
            btnBack.Text = "Đóng";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // lblTotalDebt
            // 
            lblTotalDebt.AutoSize = true;
            lblTotalDebt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalDebt.Location = new Point(30, 735);
            lblTotalDebt.Margin = new Padding(4, 0, 4, 0);
            lblTotalDebt.Name = "lblTotalDebt";
            lblTotalDebt.Size = new Size(102, 30);
            lblTotalDebt.TabIndex = 10;
            lblTotalDebt.Text = "Tổng nợ:";
            // 
            // lblTotalDebtValue
            // 
            lblTotalDebtValue.AutoSize = true;
            lblTotalDebtValue.Location = new Point(165, 735);
            lblTotalDebtValue.Margin = new Padding(4, 0, 4, 0);
            lblTotalDebtValue.Name = "lblTotalDebtValue";
            lblTotalDebtValue.Size = new Size(24, 30);
            lblTotalDebtValue.TabIndex = 11;
            lblTotalDebtValue.Text = "0";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.FromArgb(108, 117, 125);
            lblStatus.Location = new Point(30, 780);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 30);
            lblStatus.TabIndex = 12;
            // 
            // lblAdminNote
            // 
            lblAdminNote.AutoSize = true;
            lblAdminNote.ForeColor = Color.FromArgb(220, 53, 69);
            lblAdminNote.Location = new Point(540, 735);
            lblAdminNote.Margin = new Padding(4, 0, 4, 0);
            lblAdminNote.Name = "lblAdminNote";
            lblAdminNote.Size = new Size(348, 30);
            lblAdminNote.TabIndex = 13;
            lblAdminNote.Text = "Chỉ Admin mới có quyền miễn phạt.";
            lblAdminNote.Visible = false;
            // 
            // FeeForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1416, 840);
            Controls.Add(lblAdminNote);
            Controls.Add(lblStatus);
            Controls.Add(lblTotalDebtValue);
            Controls.Add(lblTotalDebt);
            Controls.Add(btnBack);
            Controls.Add(btnRefresh);
            Controls.Add(btnWaive);
            Controls.Add(btnPay);
            Controls.Add(nudAmount);
            Controls.Add(lblAmount);
            Controls.Add(dgvFees);
            Controls.Add(cboMembers);
            Controls.Add(lblMember);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "FeeForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý phí";
            ((System.ComponentModel.ISupportInitialize)dgvFees).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAmount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblMember;
        private ComboBox cboMembers;
        private DataGridView dgvFees;
        private Label lblAmount;
        private NumericUpDown nudAmount;
        private Button btnPay;
        private Button btnWaive;
        private Button btnRefresh;
        private Button btnBack;
        private Label lblTotalDebt;
        private Label lblTotalDebtValue;
        private Label lblStatus;
        private Label lblAdminNote;
    }
}
