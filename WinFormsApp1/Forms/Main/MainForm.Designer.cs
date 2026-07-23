namespace WinFormsApp1.Forms.Main
{
    partial class MainForm
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
            lblWelcome = new Label();
            btnBooks = new Button();
            btnMembers = new Button();
            btnBorrowing = new Button();
            btnUsers = new Button();
            btnReports = new Button();
            btnFees = new Button();
            btnLogout = new Button();
            btnChangePassword = new Button();
            btnAuthors = new Button();
            btnCategories = new Button();
            btnLibraryCards = new Button();
            btnPublishers = new Button();
            btnReservations = new Button();
            btnInventory = new Button();
            btnReturn = new Button();
            btnHidden2 = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblWelcome.Location = new Point(30, 30);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(840, 52);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, User";
            lblWelcome.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnBooks
            // 
            btnBooks.BackColor = Color.FromArgb(0, 122, 204);
            btnBooks.FlatStyle = FlatStyle.Flat;
            btnBooks.ForeColor = Color.White;
            btnBooks.Location = new Point(30, 120);
            btnBooks.Margin = new Padding(4);
            btnBooks.Name = "btnBooks";
            btnBooks.Size = new Size(270, 80);
            btnBooks.TabIndex = 1;
            btnBooks.Text = "Books";
            btnBooks.UseVisualStyleBackColor = false;
            btnBooks.Click += btnBooks_Click;
            // 
            // btnMembers
            // 
            btnMembers.BackColor = Color.FromArgb(40, 167, 69);
            btnMembers.FlatStyle = FlatStyle.Flat;
            btnMembers.ForeColor = Color.White;
            btnMembers.Location = new Point(315, 120);
            btnMembers.Margin = new Padding(4);
            btnMembers.Name = "btnMembers";
            btnMembers.Size = new Size(270, 80);
            btnMembers.TabIndex = 2;
            btnMembers.Text = "Members";
            btnMembers.UseVisualStyleBackColor = false;
            btnMembers.Click += btnMembers_Click;
            // 
            // btnBorrowing
            // 
            btnBorrowing.BackColor = Color.FromArgb(255, 193, 7);
            btnBorrowing.FlatStyle = FlatStyle.Flat;
            btnBorrowing.ForeColor = Color.Black;
            btnBorrowing.Location = new Point(600, 120);
            btnBorrowing.Margin = new Padding(4);
            btnBorrowing.Name = "btnBorrowing";
            btnBorrowing.Size = new Size(270, 80);
            btnBorrowing.TabIndex = 3;
            btnBorrowing.Text = "Borrowing";
            btnBorrowing.UseVisualStyleBackColor = false;
            btnBorrowing.Click += btnBorrowing_Click;
            // 
            // btnUsers
            // 
            btnUsers.BackColor = Color.FromArgb(220, 53, 69);
            btnUsers.FlatStyle = FlatStyle.Flat;
            btnUsers.ForeColor = Color.White;
            btnUsers.Location = new Point(30, 240);
            btnUsers.Margin = new Padding(4);

            btnUsers.Location = new Point(30, 210);
            btnUsers.Margin = new Padding(4, 4, 4, 4);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(270, 80);
            btnUsers.TabIndex = 4;
            btnUsers.Text = "User Management";
            btnUsers.UseVisualStyleBackColor = false;
            btnUsers.Click += btnUsers_Click;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(111, 66, 193);
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(315, 240);
            btnReports.Margin = new Padding(4);

            btnReports.Location = new Point(315, 210);
            btnReports.Margin = new Padding(4, 4, 4, 4);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(270, 80);
            btnReports.TabIndex = 5;
            btnReports.Text = "Reports";
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnReports_Click;
            // 
            // btnFees
            // 
            btnFees.BackColor = Color.FromArgb(32, 201, 151);
            btnFees.FlatStyle = FlatStyle.Flat;
            btnFees.ForeColor = Color.White;
            btnFees.Location = new Point(1284, 240);
            btnFees.Margin = new Padding(4);
            btnFees.Name = "btnFees";
            btnFees.Size = new Size(270, 75);
            btnFees.TabIndex = 11;
            btnFees.Text = "Fees";
            btnFees.UseVisualStyleBackColor = false;
            btnFees.Click += btnFees_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(108, 117, 125);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(593, 443);
            btnLogout.Margin = new Padding(4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(270, 80);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = Color.FromArgb(23, 162, 184);
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(315, 445);
            btnChangePassword.Margin = new Padding(4);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(270, 80);
            btnChangePassword.TabIndex = 7;
            btnChangePassword.Text = "Change Password";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnAuthors
            // 
            btnAuthors.BackColor = Color.FromArgb(52, 58, 64);
            btnAuthors.FlatStyle = FlatStyle.Flat;
            btnAuthors.ForeColor = Color.White;
            btnAuthors.Location = new Point(600, 240);
            btnAuthors.Margin = new Padding(4);
            btnAuthors.Name = "btnAuthors";
            btnAuthors.Size = new Size(270, 80);
            btnAuthors.TabIndex = 8;
            btnAuthors.Text = "Authors";
            btnAuthors.UseVisualStyleBackColor = false;
            btnAuthors.Click += btnAuthors_Click;
            // 
            // btnCategories
            // 
            btnCategories.BackColor = Color.FromArgb(73, 80, 87);
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.ForeColor = Color.White;
            btnCategories.Location = new Point(30, 360);
            btnCategories.Margin = new Padding(4);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(270, 80);
            btnCategories.TabIndex = 9;
            btnCategories.Text = "Categories";
            btnCategories.UseVisualStyleBackColor = false;
            btnCategories.Click += btnCategories_Click;
            // 
            // btnLibraryCards
            // 
            btnLibraryCards.BackColor = Color.FromArgb(0, 123, 167);
            btnLibraryCards.FlatStyle = FlatStyle.Flat;
            btnLibraryCards.ForeColor = Color.White;
            btnLibraryCards.Location = new Point(600, 300);
            btnLibraryCards.Margin = new Padding(4, 4, 4, 4);
            btnLibraryCards.Name = "btnLibraryCards";
            btnLibraryCards.Size = new Size(270, 80);
            btnLibraryCards.TabIndex = 10;
            btnLibraryCards.Text = "Library Cards";
            btnLibraryCards.UseVisualStyleBackColor = false;
            btnLibraryCards.Click += btnLibraryCards_Click;
            // 
            // btnPublishers
            // 
            btnPublishers.BackColor = Color.FromArgb(102, 16, 242);
            btnPublishers.FlatStyle = FlatStyle.Flat;
            btnPublishers.ForeColor = Color.White;
            btnPublishers.Location = new Point(315, 360);
            btnPublishers.Margin = new Padding(4);
            btnPublishers.Name = "btnPublishers";
            btnPublishers.Size = new Size(270, 80);
            btnPublishers.TabIndex = 11;
            btnPublishers.Text = "Publishers";
            btnPublishers.UseVisualStyleBackColor = false;
            btnPublishers.Click += btnPublishers_Click;
            // 
            // btnReservations
            // 
            btnReservations.BackColor = Color.FromArgb(253, 126, 20);
            btnReservations.FlatStyle = FlatStyle.Flat;
            btnReservations.ForeColor = Color.White;
            btnReservations.Location = new Point(30, 390);
            btnReservations.Margin = new Padding(4, 4, 4, 4);
            btnReservations.Name = "btnReservations";
            btnReservations.Size = new Size(270, 80);
            btnReservations.TabIndex = 12;
            btnReservations.Text = "Reservations";
            btnReservations.UseVisualStyleBackColor = false;
            btnReservations.Click += btnReservations_Click;
            // 
            // btnInventory
            // 
            btnInventory.BackColor = Color.FromArgb(23, 162, 184);
            btnInventory.FlatStyle = FlatStyle.Flat;
            btnInventory.ForeColor = Color.White;
            btnInventory.Location = new Point(600, 390);
            btnInventory.Margin = new Padding(4, 4, 4, 4);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(270, 80);
            btnInventory.TabIndex = 13;
            btnInventory.Text = "Inventory";
            btnInventory.UseVisualStyleBackColor = false;
            btnInventory.Click += btnInventory_Click;
            // 
            // btnReturn
            // 
            btnReturn.BackColor = Color.FromArgb(23, 162, 184);
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.ForeColor = Color.White;
            btnReturn.Location = new Point(30, 480);
            btnReturn.Margin = new Padding(4, 4, 4, 4);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(270, 80);
            btnReturn.TabIndex = 14;
            btnReturn.Text = "Return Book";
            btnReturn.UseVisualStyleBackColor = false;
            btnReturn.Click += btnReturn_Click;
            // 
            // btnHidden2
            // 
            btnHidden2.BackColor = Color.FromArgb(108, 117, 125);
            btnHidden2.FlatStyle = FlatStyle.Flat;
            btnHidden2.ForeColor = Color.White;
            btnHidden2.Location = new Point(315, 480);
            btnHidden2.Margin = new Padding(4, 4, 4, 4);
            btnHidden2.Name = "btnHidden2";
            btnHidden2.Size = new Size(270, 80);
            btnHidden2.TabIndex = 15;
            btnHidden2.Text = "Hidden 2";
            btnHidden2.UseVisualStyleBackColor = false;
            btnHidden2.Visible = false;
            //
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1596, 625);
            Controls.Add(lblWelcome);
            Controls.Add(btnBooks);
            Controls.Add(btnMembers);
            Controls.Add(btnBorrowing);
            Controls.Add(btnUsers);
            Controls.Add(btnReports);
            Controls.Add(btnLogout);
            Controls.Add(btnChangePassword);
            Controls.Add(btnAuthors);
            Controls.Add(btnCategories);
            Controls.Add(btnLibraryCards);
            Controls.Add(btnPublishers);
            Controls.Add(btnFees);
            Controls.Add(btnReservations);
            Controls.Add(btnInventory);
            Controls.Add(btnReturn);
            Controls.Add(btnHidden2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Library Management System";
            ResumeLayout(false);
        }

        #endregion

        private Label lblWelcome;
        private Button btnBooks;
        private Button btnMembers;
        private Button btnBorrowing;
        private Button btnUsers;
        private Button btnReports;
        private Button btnLogout;
        private Button btnChangePassword;
        private Button btnAuthors;
        private Button btnCategories;
        private Button btnLibraryCards;
        private Button btnPublishers;
        private Button btnFees;
        private Button btnReservations;
        private Button btnInventory;
        private Button btnReturn;
        private Button btnHidden2;
    }
}
