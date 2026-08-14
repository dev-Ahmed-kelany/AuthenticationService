namespace AuthenticationService.WinForms.Login
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlMain;
        private Panel pnlLeft;
        private Panel pnlRight;
        private Panel pnlStatusBar;
        private Label lblApplicationName;
        private Label lblApplicationDescription;
        private Label lblInternalUse;
        private Label lblVersion;

        private Label lblSignIn;
        private Label lblDescription;
        private Label lblUsername;
        private Label lblPassword;

        private TextBox txtUsername;
        private TextBox txtPassword;

        private Button btnLogin;

        private Label lblStatus;
        private Label lblSystemUser;

        private Panel pnlAccent;
        private Panel pnlSeparator;

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
            pnlMain = new Panel();
            pnlRight = new Panel();
            btnLogin = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblDescription = new Label();
            lblSignIn = new Label();
            pnlLeft = new Panel();
            lblVersion = new Label();
            lblInternalUse = new Label();
            pnlSeparator = new Panel();
            lblApplicationDescription = new Label();
            lblApplicationName = new Label();
            pnlAccent = new Panel();
            pnlStatusBar = new Panel();
            lblSystemUser = new Label();
            lblStatus = new Label();
            pnlMain.SuspendLayout();
            pnlRight.SuspendLayout();
            pnlLeft.SuspendLayout();
            pnlStatusBar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(pnlRight);
            pnlMain.Controls.Add(pnlLeft);
            pnlMain.Controls.Add(pnlStatusBar);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(780, 568);
            pnlMain.TabIndex = 0;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.White;
            pnlRight.Controls.Add(btnLogin);
            pnlRight.Controls.Add(txtPassword);
            pnlRight.Controls.Add(lblPassword);
            pnlRight.Controls.Add(txtUsername);
            pnlRight.Controls.Add(lblUsername);
            pnlRight.Controls.Add(lblDescription);
            pnlRight.Controls.Add(lblSignIn);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(288, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(64, 0, 64, 0);
            pnlRight.Size = new Size(490, 544);
            pnlRight.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(37, 99, 235);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(141, 390);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 32);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Log In";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.FromArgb(55, 65, 81);
            txtPassword.Location = new Point(141, 346);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(300, 25);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(55, 65, 81);
            lblPassword.Location = new Point(142, 325);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(59, 15);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.ForeColor = Color.FromArgb(55, 65, 81);
            txtUsername.Location = new Point(141, 281);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 25);
            txtUsername.TabIndex = 0;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(55, 65, 81);
            lblUsername.Location = new Point(142, 260);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(64, 15);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Username";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.ForeColor = Color.FromArgb(107, 114, 128);
            lblDescription.Location = new Point(142, 210);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(188, 15);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Enter your credentials to continue.";
            // 
            // lblSignIn
            // 
            lblSignIn.AutoSize = true;
            lblSignIn.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblSignIn.ForeColor = Color.FromArgb(30, 58, 95);
            lblSignIn.Location = new Point(141, 176);
            lblSignIn.Name = "lblSignIn";
            lblSignIn.Size = new Size(105, 37);
            lblSignIn.TabIndex = 6;
            lblSignIn.Text = "Sign In";
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(243, 244, 246);
            pnlLeft.Controls.Add(lblVersion);
            pnlLeft.Controls.Add(lblInternalUse);
            pnlLeft.Controls.Add(pnlSeparator);
            pnlLeft.Controls.Add(lblApplicationDescription);
            pnlLeft.Controls.Add(lblApplicationName);
            pnlLeft.Controls.Add(pnlAccent);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(36, 0, 36, 0);
            pnlLeft.Size = new Size(288, 544);
            pnlLeft.TabIndex = 1;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.FromArgb(156, 163, 175);
            lblVersion.Location = new Point(36, 358);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(72, 13);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "Version 1.0.0";
            // 
            // lblInternalUse
            // 
            lblInternalUse.AutoSize = true;
            lblInternalUse.Font = new Font("Segoe UI", 8F);
            lblInternalUse.ForeColor = Color.FromArgb(156, 163, 175);
            lblInternalUse.Location = new Point(36, 339);
            lblInternalUse.Name = "lblInternalUse";
            lblInternalUse.Size = new Size(93, 13);
            lblInternalUse.TabIndex = 1;
            lblInternalUse.Text = "Internal use only";
            // 
            // pnlSeparator
            // 
            pnlSeparator.BackColor = Color.FromArgb(209, 213, 219);
            pnlSeparator.Location = new Point(36, 320);
            pnlSeparator.Name = "pnlSeparator";
            pnlSeparator.Size = new Size(216, 1);
            pnlSeparator.TabIndex = 2;
            // 
            // lblApplicationDescription
            // 
            lblApplicationDescription.AutoSize = true;
            lblApplicationDescription.Font = new Font("Segoe UI", 9F);
            lblApplicationDescription.ForeColor = Color.FromArgb(107, 114, 128);
            lblApplicationDescription.Location = new Point(36, 278);
            lblApplicationDescription.Name = "lblApplicationDescription";
            lblApplicationDescription.Size = new Size(164, 15);
            lblApplicationDescription.TabIndex = 3;
            lblApplicationDescription.Text = "Authentication & Authorization";
            // 
            // lblApplicationName
            // 
            lblApplicationName.AutoSize = true;
            lblApplicationName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblApplicationName.ForeColor = Color.FromArgb(30, 58, 95);
            lblApplicationName.Location = new Point(36, 221);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new Size(167, 60);
            lblApplicationName.TabIndex = 4;
            lblApplicationName.Text = "Authentication\r\nService";
            // 
            // pnlAccent
            // 
            pnlAccent.BackColor = Color.FromArgb(37, 99, 235);
            pnlAccent.Location = new Point(36, 200);
            pnlAccent.Name = "pnlAccent";
            pnlAccent.Size = new Size(32, 3);
            pnlAccent.TabIndex = 5;
            // 
            // pnlStatusBar
            // 
            pnlStatusBar.BackColor = Color.FromArgb(243, 244, 246);
            pnlStatusBar.Controls.Add(lblSystemUser);
            pnlStatusBar.Controls.Add(lblStatus);
            pnlStatusBar.Dock = DockStyle.Bottom;
            pnlStatusBar.Location = new Point(0, 544);
            pnlStatusBar.Name = "pnlStatusBar";
            pnlStatusBar.Size = new Size(778, 22);
            pnlStatusBar.TabIndex = 3;
            // 
            // lblSystemUser
            // 
            lblSystemUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSystemUser.AutoSize = true;
            lblSystemUser.Font = new Font("Segoe UI", 8F);
            lblSystemUser.ForeColor = Color.FromArgb(156, 163, 175);
            lblSystemUser.Location = new Point(1338, 3);
            lblSystemUser.Name = "lblSystemUser";
            lblSystemUser.Size = new Size(122, 13);
            lblSystemUser.TabIndex = 0;
            lblSystemUser.Text = "Authentication Service";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 8F);
            lblStatus.ForeColor = Color.FromArgb(107, 114, 128);
            lblStatus.Location = new Point(10, 3);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 13);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Ready";
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(780, 568);
            Controls.Add(pnlMain);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Authentication Service";
            pnlMain.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            pnlStatusBar.ResumeLayout(false);
            pnlStatusBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}