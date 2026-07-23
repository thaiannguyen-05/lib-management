namespace WinFormsApp1.Forms.Inventory
{
    partial class InventoryForm
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
            lblAvailable = new Label();
            lblBorrowed = new Label();
            lblDamaged = new Label();
            lblLost = new Label();
            lblTotal = new Label();
            grpImport = new GroupBox();
            cmbBook = new ComboBox();
            lblBook = new Label();
            txtQuantity = new TextBox();
            lblQuantity = new Label();
            btnImport = new Button();
            grpActions = new GroupBox();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblNewShelf = new Label();
            txtNewShelf = new TextBox();
            lblReason = new Label();
            txtReason = new TextBox();
            btnTransfer = new Button();
            btnDispose = new Button();
            btnReportLost = new Button();
            btnReportDamaged = new Button();
            lblFilterAction = new Label();
            cmbFilterAction = new ComboBox();
            dgvLogs = new DataGridView();
            btnClear = new Button();
            btnBack = new Button();
            grpImport.SuspendLayout();
            grpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Inventory Management";
            // 
            // lblAvailable
            // 
            lblAvailable.AutoSize = true;
            lblAvailable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAvailable.ForeColor = Color.FromArgb(40, 167, 69);
            lblAvailable.Location = new Point(20, 55);
            lblAvailable.Name = "lblAvailable";
            lblAvailable.Size = new Size(80, 20);
            lblAvailable.TabIndex = 1;
            lblAvailable.Text = "Available: 0";
            // 
            // lblBorrowed
            // 
            lblBorrowed.AutoSize = true;
            lblBorrowed.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBorrowed.ForeColor = Color.FromArgb(0, 122, 204);
            lblBorrowed.Location = new Point(140, 55);
            lblBorrowed.Name = "lblBorrowed";
            lblBorrowed.Size = new Size(80, 20);
            lblBorrowed.TabIndex = 2;
            lblBorrowed.Text = "Borrowed: 0";
            // 
            // lblDamaged
            // 
            lblDamaged.AutoSize = true;
            lblDamaged.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDamaged.ForeColor = Color.FromArgb(255, 193, 7);
            lblDamaged.Location = new Point(260, 55);
            lblDamaged.Name = "lblDamaged";
            lblDamaged.Size = new Size(80, 20);
            lblDamaged.TabIndex = 3;
            lblDamaged.Text = "Damaged: 0";
            // 
            // lblLost
            // 
            lblLost.AutoSize = true;
            lblLost.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLost.ForeColor = Color.FromArgb(220, 53, 69);
            lblLost.Location = new Point(380, 55);
            lblLost.Name = "lblLost";
            lblLost.Size = new Size(60, 20);
            lblLost.TabIndex = 4;
            lblLost.Text = "Lost: 0";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.Location = new Point(480, 55);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(60, 20);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "Total: 0";
            // 
            // grpImport
            // 
            grpImport.Controls.Add(cmbBook);
            grpImport.Controls.Add(lblBook);
            grpImport.Controls.Add(txtQuantity);
            grpImport.Controls.Add(lblQuantity);
            grpImport.Controls.Add(btnImport);
            grpImport.Location = new Point(20, 85);
            grpImport.Name = "grpImport";
            grpImport.Size = new Size(360, 90);
            grpImport.TabIndex = 6;
            grpImport.TabStop = false;
            grpImport.Text = "Import Books";
            // 
            // cmbBook
            // 
            cmbBook.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBook.Location = new Point(60, 25);
            cmbBook.Name = "cmbBook";
            cmbBook.Size = new Size(170, 28);
            cmbBook.TabIndex = 1;
            // 
            // lblBook
            // 
            lblBook.AutoSize = true;
            lblBook.Location = new Point(10, 28);
            lblBook.Name = "lblBook";
            lblBook.Size = new Size(42, 20);
            lblBook.TabIndex = 0;
            lblBook.Text = "Book:";
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(290, 25);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(55, 27);
            txtQuantity.TabIndex = 3;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(240, 28);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(42, 20);
            lblQuantity.TabIndex = 2;
            lblQuantity.Text = "Qty:";
            // 
            // btnImport
            // 
            btnImport.BackColor = Color.FromArgb(40, 167, 69);
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.ForeColor = Color.White;
            btnImport.Location = new Point(60, 55);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(130, 28);
            btnImport.TabIndex = 4;
            btnImport.Text = "Import";
            btnImport.UseVisualStyleBackColor = false;
            btnImport.Click += btnImport_Click;
            // 
            // grpActions
            // 
            grpActions.Controls.Add(lblBarcode);
            grpActions.Controls.Add(txtBarcode);
            grpActions.Controls.Add(lblNewShelf);
            grpActions.Controls.Add(txtNewShelf);
            grpActions.Controls.Add(lblReason);
            grpActions.Controls.Add(txtReason);
            grpActions.Controls.Add(btnTransfer);
            grpActions.Controls.Add(btnDispose);
            grpActions.Controls.Add(btnReportLost);
            grpActions.Controls.Add(btnReportDamaged);
            grpActions.Location = new Point(400, 85);
            grpActions.Name = "grpActions";
            grpActions.Size = new Size(370, 120);
            grpActions.TabIndex = 7;
            grpActions.TabStop = false;
            grpActions.Text = "Copy Actions (by Barcode)";
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Location = new Point(10, 28);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(65, 20);
            lblBarcode.TabIndex = 0;
            lblBarcode.Text = "Barcode:";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(80, 25);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(140, 27);
            txtBarcode.TabIndex = 1;
            // 
            // lblNewShelf
            // 
            lblNewShelf.AutoSize = true;
            lblNewShelf.Location = new Point(230, 28);
            lblNewShelf.Name = "lblNewShelf";
            lblNewShelf.Size = new Size(45, 20);
            lblNewShelf.TabIndex = 2;
            lblNewShelf.Text = "Shelf:";
            // 
            // txtNewShelf
            // 
            txtNewShelf.Location = new Point(280, 25);
            txtNewShelf.Name = "txtNewShelf";
            txtNewShelf.Size = new Size(80, 27);
            txtNewShelf.TabIndex = 3;
            // 
            // lblReason
            // 
            lblReason.AutoSize = true;
            lblReason.Location = new Point(10, 58);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(52, 20);
            lblReason.TabIndex = 8;
            lblReason.Text = "Reason:";
            // 
            // txtReason
            // 
            txtReason.Location = new Point(70, 55);
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(140, 27);
            txtReason.TabIndex = 9;
            // 
            // btnTransfer
            // 
            btnTransfer.BackColor = Color.FromArgb(0, 122, 204);
            btnTransfer.FlatStyle = FlatStyle.Flat;
            btnTransfer.ForeColor = Color.White;
            btnTransfer.Location = new Point(10, 55);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new Size(80, 28);
            btnTransfer.TabIndex = 4;
            btnTransfer.Text = "Transfer";
            btnTransfer.UseVisualStyleBackColor = false;
            btnTransfer.Click += btnTransfer_Click;
            // 
            // btnDispose
            // 
            btnDispose.BackColor = Color.FromArgb(108, 117, 125);
            btnDispose.FlatStyle = FlatStyle.Flat;
            btnDispose.ForeColor = Color.White;
            btnDispose.Location = new Point(100, 55);
            btnDispose.Name = "btnDispose";
            btnDispose.Size = new Size(80, 28);
            btnDispose.TabIndex = 5;
            btnDispose.Text = "Dispose";
            btnDispose.UseVisualStyleBackColor = false;
            btnDispose.Click += btnDispose_Click;
            // 
            // btnReportLost
            // 
            btnReportLost.BackColor = Color.FromArgb(220, 53, 69);
            btnReportLost.FlatStyle = FlatStyle.Flat;
            btnReportLost.ForeColor = Color.White;
            btnReportLost.Location = new Point(190, 55);
            btnReportLost.Name = "btnReportLost";
            btnReportLost.Size = new Size(80, 28);
            btnReportLost.TabIndex = 6;
            btnReportLost.Text = "Lost";
            btnReportLost.UseVisualStyleBackColor = false;
            btnReportLost.Click += btnReportLost_Click;
            // 
            // btnReportDamaged
            // 
            btnReportDamaged.BackColor = Color.FromArgb(255, 193, 7);
            btnReportDamaged.FlatStyle = FlatStyle.Flat;
            btnReportDamaged.ForeColor = Color.Black;
            btnReportDamaged.Location = new Point(280, 55);
            btnReportDamaged.Name = "btnReportDamaged";
            btnReportDamaged.Size = new Size(80, 28);
            btnReportDamaged.TabIndex = 7;
            btnReportDamaged.Text = "Damaged";
            btnReportDamaged.UseVisualStyleBackColor = false;
            btnReportDamaged.Click += btnReportDamaged_Click;
            // 
            // lblFilterAction
            // 
            lblFilterAction.AutoSize = true;
            lblFilterAction.Location = new Point(20, 190);
            lblFilterAction.Name = "lblFilterAction";
            lblFilterAction.Size = new Size(50, 20);
            lblFilterAction.TabIndex = 8;
            lblFilterAction.Text = "Filter:";
            // 
            // cmbFilterAction
            // 
            cmbFilterAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterAction.Items.AddRange(new object[] { "All Actions", "Import", "Dispose", "Transfer", "Count", "Lost", "Damaged" });
            cmbFilterAction.Location = new Point(80, 187);
            cmbFilterAction.Name = "cmbFilterAction";
            cmbFilterAction.Size = new Size(150, 28);
            cmbFilterAction.TabIndex = 9;
            cmbFilterAction.SelectedIndexChanged += cmbFilterAction_SelectedIndexChanged;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(20, 225);
            dgvLogs.MultiSelect = false;
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersWidth = 51;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(750, 300);
            dgvLogs.TabIndex = 10;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(560, 540);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 35);
            btnClear.TabIndex = 11;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(0, 122, 204);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(670, 540);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 35);
            btnBack.TabIndex = 12;
            btnBack.Text = "Back to Menu";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // InventoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 590);
            Controls.Add(lblTitle);
            Controls.Add(lblAvailable);
            Controls.Add(lblBorrowed);
            Controls.Add(lblDamaged);
            Controls.Add(lblLost);
            Controls.Add(lblTotal);
            Controls.Add(grpImport);
            Controls.Add(grpActions);
            Controls.Add(lblFilterAction);
            Controls.Add(cmbFilterAction);
            Controls.Add(dgvLogs);
            Controls.Add(btnClear);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "InventoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Inventory Management";
            grpImport.ResumeLayout(false);
            grpImport.PerformLayout();
            grpActions.ResumeLayout(false);
            grpActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblAvailable;
        private Label lblBorrowed;
        private Label lblDamaged;
        private Label lblLost;
        private Label lblTotal;
        private GroupBox grpImport;
        private ComboBox cmbBook;
        private Label lblBook;
        private TextBox txtQuantity;
        private Label lblQuantity;
        private Button btnImport;
        private GroupBox grpActions;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblNewShelf;
        private TextBox txtNewShelf;
        private Label lblReason;
        private TextBox txtReason;
        private Button btnTransfer;
        private Button btnDispose;
        private Button btnReportLost;
        private Button btnReportDamaged;
        private Label lblFilterAction;
        private ComboBox cmbFilterAction;
        private DataGridView dgvLogs;
        private Button btnClear;
        private Button btnBack;
    }
}
