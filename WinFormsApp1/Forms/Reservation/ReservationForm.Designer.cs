namespace WinFormsApp1.Forms.Reservations
{
    partial class ReservationForm
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
            dgvReservations = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            BookTitle = new DataGridViewTextBoxColumn();
            MemberName = new DataGridViewTextBoxColumn();
            ReservationDate = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            ExpiryDate = new DataGridViewTextBoxColumn();
            lblBook = new Label();
            cmbBook = new ComboBox();
            lblMember = new Label();
            cmbMember = new ComboBox();
            btnReserve = new Button();
            btnCancel = new Button();
            btnFulfill = new Button();
            btnCollect = new Button();
            btnComplete = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReservations).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(750, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Reservation Management";
            //
            // dgvReservations
            //
            dgvReservations.AllowUserToAddRows = false;
            dgvReservations.AllowUserToDeleteRows = false;
            dgvReservations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReservations.Columns.AddRange(new DataGridViewColumn[] { Id, BookTitle, MemberName, ReservationDate, Status, ExpiryDate });
            dgvReservations.Location = new Point(20, 50);
            dgvReservations.MultiSelect = false;
            dgvReservations.Name = "dgvReservations";
            dgvReservations.ReadOnly = true;
            dgvReservations.RowHeadersWidth = 51;
            dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservations.Size = new Size(750, 300);
            dgvReservations.TabIndex = 1;
            dgvReservations.CellClick += dgvReservations_CellClick;
            //
            // Id
            //
            Id.HeaderText = "ID";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Width = 50;
            //
            // BookTitle
            //
            BookTitle.HeaderText = "Book";
            BookTitle.MinimumWidth = 6;
            BookTitle.Name = "BookTitle";
            BookTitle.ReadOnly = true;
            //
            // MemberName
            //
            MemberName.HeaderText = "Member";
            MemberName.MinimumWidth = 6;
            MemberName.Name = "MemberName";
            MemberName.ReadOnly = true;
            //
            // ReservationDate
            //
            ReservationDate.HeaderText = "Reservation Date";
            ReservationDate.MinimumWidth = 6;
            ReservationDate.Name = "ReservationDate";
            ReservationDate.ReadOnly = true;
            //
            // Status
            //
            Status.HeaderText = "Status";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.Width = 100;
            //
            // ExpiryDate
            //
            ExpiryDate.HeaderText = "Expiry Date";
            ExpiryDate.MinimumWidth = 6;
            ExpiryDate.Name = "ExpiryDate";
            ExpiryDate.ReadOnly = true;
            //
            // lblBook
            //
            lblBook.AutoSize = true;
            lblBook.Location = new Point(20, 370);
            lblBook.Name = "lblBook";
            lblBook.Size = new Size(42, 20);
            lblBook.TabIndex = 2;
            lblBook.Text = "Book:";
            //
            // cmbBook
            //
            cmbBook.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBook.Location = new Point(80, 367);
            cmbBook.Name = "cmbBook";
            cmbBook.Size = new Size(300, 28);
            cmbBook.TabIndex = 3;
            //
            // lblMember
            //
            lblMember.AutoSize = true;
            lblMember.Location = new Point(20, 410);
            lblMember.Name = "lblMember";
            lblMember.Size = new Size(67, 20);
            lblMember.TabIndex = 4;
            lblMember.Text = "Member:";
            //
            // cmbMember
            //
            cmbMember.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMember.Location = new Point(80, 407);
            cmbMember.Name = "cmbMember";
            cmbMember.Size = new Size(300, 28);
            cmbMember.TabIndex = 5;
            //
            // btnReserve
            //
            btnReserve.BackColor = Color.FromArgb(40, 167, 69);
            btnReserve.FlatStyle = FlatStyle.Flat;
            btnReserve.ForeColor = Color.White;
            btnReserve.Location = new Point(20, 460);
            btnReserve.Name = "btnReserve";
            btnReserve.Size = new Size(120, 38);
            btnReserve.TabIndex = 6;
            btnReserve.Text = "Reserve";
            btnReserve.UseVisualStyleBackColor = false;
            btnReserve.Click += btnReserve_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.FromArgb(220, 53, 69);
            btnCancel.Enabled = false;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(150, 460);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 38);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // btnFulfill
            //
            btnFulfill.BackColor = Color.FromArgb(0, 122, 204);
            btnFulfill.Enabled = false;
            btnFulfill.FlatStyle = FlatStyle.Flat;
            btnFulfill.ForeColor = Color.White;
            btnFulfill.Location = new Point(280, 460);
            btnFulfill.Name = "btnFulfill";
            btnFulfill.Size = new Size(120, 38);
            btnFulfill.TabIndex = 8;
            btnFulfill.Text = "Fulfill";
            btnFulfill.UseVisualStyleBackColor = false;
            btnFulfill.Click += btnFulfill_Click;
            //
            // btnCollect
            //
            btnCollect.BackColor = Color.FromArgb(255, 193, 7);
            btnCollect.Enabled = false;
            btnCollect.FlatStyle = FlatStyle.Flat;
            btnCollect.ForeColor = Color.Black;
            btnCollect.Location = new Point(410, 460);
            btnCollect.Name = "btnCollect";
            btnCollect.Size = new Size(120, 38);
            btnCollect.TabIndex = 9;
            btnCollect.Text = "Collect";
            btnCollect.UseVisualStyleBackColor = false;
            btnCollect.Click += btnCollect_Click;
            //
            // btnComplete
            //
            btnComplete.BackColor = Color.FromArgb(23, 162, 184);
            btnComplete.Enabled = false;
            btnComplete.FlatStyle = FlatStyle.Flat;
            btnComplete.ForeColor = Color.White;
            btnComplete.Location = new Point(540, 460);
            btnComplete.Name = "btnComplete";
            btnComplete.Size = new Size(120, 38);
            btnComplete.TabIndex = 10;
            btnComplete.Text = "Complete";
            btnComplete.UseVisualStyleBackColor = false;
            btnComplete.Click += btnComplete_Click;
            //
            // btnBack
            //
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(650, 460);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(120, 38);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            //
            // ReservationForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 520);
            Controls.Add(lblTitle);
            Controls.Add(dgvReservations);
            Controls.Add(lblBook);
            Controls.Add(cmbBook);
            Controls.Add(lblMember);
            Controls.Add(cmbMember);
            Controls.Add(btnReserve);
            Controls.Add(btnCancel);
            Controls.Add(btnFulfill);
            Controls.Add(btnCollect);
            Controls.Add(btnComplete);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ReservationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reservation Management";
            ((System.ComponentModel.ISupportInitialize)dgvReservations).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvReservations;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn BookTitle;
        private DataGridViewTextBoxColumn MemberName;
        private DataGridViewTextBoxColumn ReservationDate;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn ExpiryDate;
        private Label lblBook;
        private ComboBox cmbBook;
        private Label lblMember;
        private ComboBox cmbMember;
        private Button btnReserve;
        private Button btnCancel;
        private Button btnFulfill;
        private Button btnCollect;
        private Button btnComplete;
        private Button btnBack;
    }
}
