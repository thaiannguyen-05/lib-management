namespace WinFormsApp1.Forms.Report
{
    partial class ReportForm
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
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            btnRefresh = new Button();
            btnExportCsv = new Button();
            tabReports = new TabControl();
            tabBorrowed = new TabPage();
            dgvBorrowed = new DataGridView();
            tabOverdue = new TabPage();
            dgvOverdue = new DataGridView();
            tabMostBorrowed = new TabPage();
            dgvMostBorrowed = new DataGridView();
            tabMostActive = new TabPage();
            dgvMostActive = new DataGridView();
            tabFees = new TabPage();
            lblTotalFees = new Label();
            tabLostDamaged = new TabPage();
            dgvLostDamaged = new DataGridView();
            btnBack = new Button();
            tabReports.SuspendLayout();
            tabBorrowed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBorrowed).BeginInit();
            tabOverdue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).BeginInit();
            tabMostBorrowed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMostBorrowed).BeginInit();
            tabMostActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMostActive).BeginInit();
            tabFees.SuspendLayout();
            tabLostDamaged.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLostDamaged).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Báo cáo thống kê";
            //
            // lblFrom
            //
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(20, 55);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(65, 20);
            lblFrom.TabIndex = 1;
            lblFrom.Text = "Từ ngày:";
            //
            // dtpFrom
            //
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(100, 52);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ShowCheckBox = true;
            dtpFrom.Size = new Size(150, 27);
            dtpFrom.TabIndex = 2;
            //
            // lblTo
            //
            lblTo.AutoSize = true;
            lblTo.Location = new Point(270, 55);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(75, 20);
            lblTo.TabIndex = 3;
            lblTo.Text = "Đến ngày:";
            //
            // dtpTo
            //
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(355, 52);
            dtpTo.Name = "dtpTo";
            dtpTo.ShowCheckBox = true;
            dtpTo.Size = new Size(150, 27);
            dtpTo.TabIndex = 4;
            //
            // btnRefresh
            //
            btnRefresh.BackColor = Color.FromArgb(0, 122, 204);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(530, 50);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            //
            // btnExportCsv
            //
            btnExportCsv.BackColor = Color.FromArgb(40, 167, 69);
            btnExportCsv.FlatStyle = FlatStyle.Flat;
            btnExportCsv.ForeColor = Color.White;
            btnExportCsv.Location = new Point(640, 50);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(110, 30);
            btnExportCsv.TabIndex = 6;
            btnExportCsv.Text = "Xuất CSV";
            btnExportCsv.UseVisualStyleBackColor = false;
            btnExportCsv.Click += btnExportCsv_Click;
            //
            // tabReports
            //
            tabReports.Controls.Add(tabBorrowed);
            tabReports.Controls.Add(tabOverdue);
            tabReports.Controls.Add(tabMostBorrowed);
            tabReports.Controls.Add(tabMostActive);
            tabReports.Controls.Add(tabFees);
            tabReports.Controls.Add(tabLostDamaged);
            tabReports.Location = new Point(20, 95);
            tabReports.Name = "tabReports";
            tabReports.SelectedIndex = 0;
            tabReports.Size = new Size(730, 440);
            tabReports.TabIndex = 7;
            //
            // tabBorrowed
            //
            tabBorrowed.Controls.Add(dgvBorrowed);
            tabBorrowed.Location = new Point(4, 29);
            tabBorrowed.Name = "tabBorrowed";
            tabBorrowed.Padding = new Padding(3);
            tabBorrowed.Size = new Size(722, 407);
            tabBorrowed.TabIndex = 0;
            tabBorrowed.Text = "Sách đang mượn";
            tabBorrowed.UseVisualStyleBackColor = true;
            //
            // dgvBorrowed
            //
            dgvBorrowed.AllowUserToAddRows = false;
            dgvBorrowed.AllowUserToDeleteRows = false;
            dgvBorrowed.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBorrowed.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBorrowed.Dock = DockStyle.Fill;
            dgvBorrowed.Location = new Point(3, 3);
            dgvBorrowed.MultiSelect = false;
            dgvBorrowed.Name = "dgvBorrowed";
            dgvBorrowed.ReadOnly = true;
            dgvBorrowed.RowHeadersWidth = 51;
            dgvBorrowed.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBorrowed.Size = new Size(716, 401);
            dgvBorrowed.TabIndex = 0;
            //
            // tabOverdue
            //
            tabOverdue.Controls.Add(dgvOverdue);
            tabOverdue.Location = new Point(4, 29);
            tabOverdue.Name = "tabOverdue";
            tabOverdue.Padding = new Padding(3);
            tabOverdue.Size = new Size(722, 407);
            tabOverdue.TabIndex = 1;
            tabOverdue.Text = "Sách quá hạn";
            tabOverdue.UseVisualStyleBackColor = true;
            //
            // dgvOverdue
            //
            dgvOverdue.AllowUserToAddRows = false;
            dgvOverdue.AllowUserToDeleteRows = false;
            dgvOverdue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOverdue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOverdue.Dock = DockStyle.Fill;
            dgvOverdue.Location = new Point(3, 3);
            dgvOverdue.MultiSelect = false;
            dgvOverdue.Name = "dgvOverdue";
            dgvOverdue.ReadOnly = true;
            dgvOverdue.RowHeadersWidth = 51;
            dgvOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOverdue.Size = new Size(716, 401);
            dgvOverdue.TabIndex = 0;
            //
            // tabMostBorrowed
            //
            tabMostBorrowed.Controls.Add(dgvMostBorrowed);
            tabMostBorrowed.Location = new Point(4, 29);
            tabMostBorrowed.Name = "tabMostBorrowed";
            tabMostBorrowed.Padding = new Padding(3);
            tabMostBorrowed.Size = new Size(722, 407);
            tabMostBorrowed.TabIndex = 2;
            tabMostBorrowed.Text = "Sách mượn nhiều nhất";
            tabMostBorrowed.UseVisualStyleBackColor = true;
            //
            // dgvMostBorrowed
            //
            dgvMostBorrowed.AllowUserToAddRows = false;
            dgvMostBorrowed.AllowUserToDeleteRows = false;
            dgvMostBorrowed.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMostBorrowed.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMostBorrowed.Dock = DockStyle.Fill;
            dgvMostBorrowed.Location = new Point(3, 3);
            dgvMostBorrowed.MultiSelect = false;
            dgvMostBorrowed.Name = "dgvMostBorrowed";
            dgvMostBorrowed.ReadOnly = true;
            dgvMostBorrowed.RowHeadersWidth = 51;
            dgvMostBorrowed.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMostBorrowed.Size = new Size(716, 401);
            dgvMostBorrowed.TabIndex = 0;
            //
            // tabMostActive
            //
            tabMostActive.Controls.Add(dgvMostActive);
            tabMostActive.Location = new Point(4, 29);
            tabMostActive.Name = "tabMostActive";
            tabMostActive.Padding = new Padding(3);
            tabMostActive.Size = new Size(722, 407);
            tabMostActive.TabIndex = 3;
            tabMostActive.Text = "Độc giả mượn nhiều nhất";
            tabMostActive.UseVisualStyleBackColor = true;
            //
            // dgvMostActive
            //
            dgvMostActive.AllowUserToAddRows = false;
            dgvMostActive.AllowUserToDeleteRows = false;
            dgvMostActive.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMostActive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMostActive.Dock = DockStyle.Fill;
            dgvMostActive.Location = new Point(3, 3);
            dgvMostActive.MultiSelect = false;
            dgvMostActive.Name = "dgvMostActive";
            dgvMostActive.ReadOnly = true;
            dgvMostActive.RowHeadersWidth = 51;
            dgvMostActive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMostActive.Size = new Size(716, 401);
            dgvMostActive.TabIndex = 0;
            //
            // tabFees
            //
            tabFees.Controls.Add(lblTotalFees);
            tabFees.Location = new Point(4, 29);
            tabFees.Name = "tabFees";
            tabFees.Padding = new Padding(3);
            tabFees.Size = new Size(722, 407);
            tabFees.TabIndex = 4;
            tabFees.Text = "Tổng tiền phạt";
            tabFees.UseVisualStyleBackColor = true;
            //
            // lblTotalFees
            //
            lblTotalFees.Dock = DockStyle.Fill;
            lblTotalFees.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalFees.ForeColor = Color.FromArgb(220, 53, 69);
            lblTotalFees.Location = new Point(3, 3);
            lblTotalFees.Name = "lblTotalFees";
            lblTotalFees.Size = new Size(716, 401);
            lblTotalFees.TabIndex = 0;
            lblTotalFees.Text = "Tổng tiền phạt: 0 VNĐ";
            lblTotalFees.TextAlign = ContentAlignment.MiddleCenter;
            //
            // tabLostDamaged
            //
            tabLostDamaged.Controls.Add(dgvLostDamaged);
            tabLostDamaged.Location = new Point(4, 29);
            tabLostDamaged.Name = "tabLostDamaged";
            tabLostDamaged.Padding = new Padding(3);
            tabLostDamaged.Size = new Size(722, 407);
            tabLostDamaged.TabIndex = 5;
            tabLostDamaged.Text = "Sách mất/hỏng";
            tabLostDamaged.UseVisualStyleBackColor = true;
            //
            // dgvLostDamaged
            //
            dgvLostDamaged.AllowUserToAddRows = false;
            dgvLostDamaged.AllowUserToDeleteRows = false;
            dgvLostDamaged.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLostDamaged.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLostDamaged.Dock = DockStyle.Fill;
            dgvLostDamaged.Location = new Point(3, 3);
            dgvLostDamaged.MultiSelect = false;
            dgvLostDamaged.Name = "dgvLostDamaged";
            dgvLostDamaged.ReadOnly = true;
            dgvLostDamaged.RowHeadersWidth = 51;
            dgvLostDamaged.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLostDamaged.Size = new Size(716, 401);
            dgvLostDamaged.TabIndex = 0;
            //
            // btnBack
            //
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(640, 545);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(110, 35);
            btnBack.TabIndex = 8;
            btnBack.Text = "Đóng";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            //
            // ReportForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(770, 595);
            Controls.Add(lblTitle);
            Controls.Add(lblFrom);
            Controls.Add(dtpFrom);
            Controls.Add(lblTo);
            Controls.Add(dtpTo);
            Controls.Add(btnRefresh);
            Controls.Add(btnExportCsv);
            Controls.Add(tabReports);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ReportForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Báo cáo thống kê";
            tabReports.ResumeLayout(false);
            tabBorrowed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBorrowed).EndInit();
            tabOverdue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).EndInit();
            tabMostBorrowed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMostBorrowed).EndInit();
            tabMostActive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMostActive).EndInit();
            tabFees.ResumeLayout(false);
            tabLostDamaged.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLostDamaged).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Button btnRefresh;
        private Button btnExportCsv;
        private TabControl tabReports;
        private TabPage tabBorrowed;
        private DataGridView dgvBorrowed;
        private TabPage tabOverdue;
        private DataGridView dgvOverdue;
        private TabPage tabMostBorrowed;
        private DataGridView dgvMostBorrowed;
        private TabPage tabMostActive;
        private DataGridView dgvMostActive;
        private TabPage tabFees;
        private Label lblTotalFees;
        private TabPage tabLostDamaged;
        private DataGridView dgvLostDamaged;
        private Button btnBack;
    }
}
