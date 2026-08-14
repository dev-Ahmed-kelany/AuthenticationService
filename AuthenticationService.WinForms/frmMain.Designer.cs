namespace AuthenticationService.WinForms
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permissionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rolesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem auditLogToolStripMenuItem;
        private System.Windows.Forms.Label lblWelcome;

        private System.Windows.Forms.Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            usersToolStripMenuItem = new ToolStripMenuItem();
            addNewUserToolStripMenuItem = new ToolStripMenuItem();
            usersListToolStripMenuItem = new ToolStripMenuItem();
            permissionsToolStripMenuItem = new ToolStripMenuItem();
            addNewPermissionToolStripMenuItem = new ToolStripMenuItem();
            permissionsListToolStripMenuItem = new ToolStripMenuItem();
            rolesToolStripMenuItem = new ToolStripMenuItem();
            addNewRoleToolStripMenuItem = new ToolStripMenuItem();
            rolesListToolStripMenuItem = new ToolStripMenuItem();
            loginHistoryToolStripMenuItem = new ToolStripMenuItem();
            auditLogToolStripMenuItem = new ToolStripMenuItem();
            lblWelcome = new Label();
            btnLogout = new Button();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(245, 247, 250);
            menuStrip.Font = new Font("Segoe UI", 10F);
            menuStrip.Items.AddRange(new ToolStripItem[] { usersToolStripMenuItem, permissionsToolStripMenuItem, rolesToolStripMenuItem, loginHistoryToolStripMenuItem, auditLogToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1100, 27);
            menuStrip.TabIndex = 3;
            // 
            // usersToolStripMenuItem
            // 
            usersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addNewUserToolStripMenuItem, usersListToolStripMenuItem });
            usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            usersToolStripMenuItem.Size = new Size(55, 23);
            usersToolStripMenuItem.Text = "Users";
            // 
            // addNewUserToolStripMenuItem
            // 
            addNewUserToolStripMenuItem.Name = "addNewUserToolStripMenuItem";
            addNewUserToolStripMenuItem.Size = new Size(166, 24);
            addNewUserToolStripMenuItem.Text = "Add New User";
            // 
            // usersListToolStripMenuItem
            // 
            usersListToolStripMenuItem.Name = "usersListToolStripMenuItem";
            usersListToolStripMenuItem.Size = new Size(166, 24);
            usersListToolStripMenuItem.Text = "Users List";
            usersListToolStripMenuItem.Click += usersListToolStripMenuItem_Click;
            // 
            // permissionsToolStripMenuItem
            // 
            permissionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addNewPermissionToolStripMenuItem, permissionsListToolStripMenuItem });
            permissionsToolStripMenuItem.Name = "permissionsToolStripMenuItem";
            permissionsToolStripMenuItem.Size = new Size(92, 23);
            permissionsToolStripMenuItem.Text = "Permissions";
            // 
            // addNewPermissionToolStripMenuItem
            // 
            addNewPermissionToolStripMenuItem.Name = "addNewPermissionToolStripMenuItem";
            addNewPermissionToolStripMenuItem.Size = new Size(203, 24);
            addNewPermissionToolStripMenuItem.Text = "Add New Permission";
            // 
            // permissionsListToolStripMenuItem
            // 
            permissionsListToolStripMenuItem.Name = "permissionsListToolStripMenuItem";
            permissionsListToolStripMenuItem.Size = new Size(203, 24);
            permissionsListToolStripMenuItem.Text = "Permissions List";
            permissionsListToolStripMenuItem.Click += permissionsListToolStripMenuItem_Click;
            // 
            // rolesToolStripMenuItem
            // 
            rolesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addNewRoleToolStripMenuItem, rolesListToolStripMenuItem });
            rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            rolesToolStripMenuItem.Size = new Size(53, 23);
            rolesToolStripMenuItem.Text = "Roles";
            // 
            // addNewRoleToolStripMenuItem
            // 
            addNewRoleToolStripMenuItem.Name = "addNewRoleToolStripMenuItem";
            addNewRoleToolStripMenuItem.Size = new Size(180, 24);
            addNewRoleToolStripMenuItem.Text = "Add New Role";
            // 
            // rolesListToolStripMenuItem
            // 
            rolesListToolStripMenuItem.Name = "rolesListToolStripMenuItem";
            rolesListToolStripMenuItem.Size = new Size(180, 24);
            rolesListToolStripMenuItem.Text = "Roles List";
            rolesListToolStripMenuItem.Click += rolesListToolStripMenuItem_Click;
            // 
            // loginHistoryToolStripMenuItem
            // 
            loginHistoryToolStripMenuItem.Name = "loginHistoryToolStripMenuItem";
            loginHistoryToolStripMenuItem.Size = new Size(103, 23);
            loginHistoryToolStripMenuItem.Text = "Login History";
            loginHistoryToolStripMenuItem.Click += loginHistoryToolStripMenuItem_Click;
            // 
            // auditLogToolStripMenuItem
            // 
            auditLogToolStripMenuItem.Name = "auditLogToolStripMenuItem";
            auditLogToolStripMenuItem.Size = new Size(81, 23);
            auditLogToolStripMenuItem.Text = "Audit Log";
            auditLogToolStripMenuItem.Click += auditLogToolStripMenuItem_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(30, 41, 59);
            lblWelcome.Location = new Point(80, 130);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(700, 45);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome to Authentication Service";
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLogout.BackColor = Color.White;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderColor = Color.FromArgb(185, 28, 28);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogout.ForeColor = Color.FromArgb(185, 28, 28);
            btnLogout.Location = new Point(950, 570);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 45);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1100, 650);
            Controls.Add(lblWelcome);
            Controls.Add(btnLogout);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new Size(900, 550);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Authentication Service";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStripMenuItem addNewUserToolStripMenuItem;
        private ToolStripMenuItem usersListToolStripMenuItem;
        private ToolStripMenuItem addNewPermissionToolStripMenuItem;
        private ToolStripMenuItem permissionsListToolStripMenuItem;
        private ToolStripMenuItem addNewRoleToolStripMenuItem;
        private ToolStripMenuItem rolesListToolStripMenuItem;
    }
}