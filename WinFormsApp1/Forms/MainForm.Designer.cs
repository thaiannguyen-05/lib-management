namespace WinFormsApp1.Forms
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
            btnLogout = new Button();
            btnChangePassword = new Button();
            btnAuthors = new Button();
            btnCategories = new Button();
            btnLibraryCards = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblWelcome.Location = new Point(20, 20);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(560, 35);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, User";
            lblWelcome.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnBooks
            // 
            btnBooks.BackColor = Color.FromArgb(0, 122, 204);
            btnBooks.FlatStyle = FlatStyle.Flat;
            btnBooks.ForeColor = Color.White;
            btnBooks.Location = new Point(20, 80);
            btnBooks.Name = "btnBooks";
            btnBooks.Size = new Size(180, 60);
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
            btnMembers.Location = new Point(210, 80);
            btnMembers.Name = "btnMembers";
            btnMembers.Size = new Size(180, 60);
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
            btnBorrowing.Location = new Point(400, 80);
            btnBorrowing.Name = "btnBorrowing";
            btnBorrowing.Size = new Size(180, 60);
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
            btnUsers.Location = new Point(20, 160);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(180, 60);
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
            btnReports.Location = new Point(210, 160);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(180, 60);
            btnReports.TabIndex = 5;
            btnReports.Text = "Reports";
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnReports_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(108, 117, 125);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(400, 250);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(180, 40);
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
            btnChangePassword.Location = new Point(210, 250);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(180, 40);
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
            btnAuthors.Location = new Point(400, 160);
            btnAuthors.Name = "btnAuthors";
            btnAuthors.Size = new Size(180, 60);
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
            btnCategories.Location = new Point(20, 240);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(180, 50);
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
            btnLibraryCards.Location = new Point(20, 310);
            btnLibraryCards.Name = "btnLibraryCards";
            btnLibraryCards.Size = new Size(180, 50);
            btnLibraryCards.TabIndex = 10;
            btnLibraryCards.Text = "Library Cards";
            btnLibraryCards.UseVisualStyleBackColor = false;
            btnLibraryCards.Click += btnLibraryCards_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 380);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
    }
}
