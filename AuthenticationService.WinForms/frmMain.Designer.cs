namespace AuthenticationService.WinForms
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLogin = new Button();
            msMain = new MenuStrip();
            usersToolStripMenuItem = new ToolStripMenuItem();
            listUsersToolStripMenuItem = new ToolStripMenuItem();
            addNewUserToolStripMenuItem = new ToolStripMenuItem();
            userDetailsToolStripMenuItem = new ToolStripMenuItem();
            permissionsToolStripMenuItem = new ToolStripMenuItem();
            listPermissionsToolStripMenuItem = new ToolStripMenuItem();
            addNewPermissionToolStripMenuItem = new ToolStripMenuItem();
            rolesToolStripMenuItem = new ToolStripMenuItem();
            listRolesToolStripMenuItem = new ToolStripMenuItem();
            addNewRoleToolStripMenuItem = new ToolStripMenuItem();
            roleDetailsToolStripMenuItem = new ToolStripMenuItem();
            loginHistoryToolStripMenuItem = new ToolStripMenuItem();
            auditLogToolStripMenuItem = new ToolStripMenuItem();
            msMain.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.AutoSize = true;
            btnLogin.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(977, 517);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(150, 46);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Logout";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // msMain
            // 
            msMain.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            msMain.Items.AddRange(new ToolStripItem[] { usersToolStripMenuItem, permissionsToolStripMenuItem, rolesToolStripMenuItem, loginHistoryToolStripMenuItem, auditLogToolStripMenuItem });
            msMain.Location = new Point(0, 0);
            msMain.Name = "msMain";
            msMain.Size = new Size(1139, 44);
            msMain.TabIndex = 6;
            msMain.Text = "menuStrip1";
            // 
            // usersToolStripMenuItem
            // 
            usersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { listUsersToolStripMenuItem, addNewUserToolStripMenuItem, userDetailsToolStripMenuItem });
            usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            usersToolStripMenuItem.Size = new Size(102, 40);
            usersToolStripMenuItem.Text = "Users";
            // 
            // listUsersToolStripMenuItem
            // 
            listUsersToolStripMenuItem.Name = "listUsersToolStripMenuItem";
            listUsersToolStripMenuItem.Size = new Size(288, 40);
            listUsersToolStripMenuItem.Text = "List Users";
            listUsersToolStripMenuItem.Click += listUsersToolStripMenuItem_Click;
            // 
            // addNewUserToolStripMenuItem
            // 
            addNewUserToolStripMenuItem.Name = "addNewUserToolStripMenuItem";
            addNewUserToolStripMenuItem.Size = new Size(288, 40);
            addNewUserToolStripMenuItem.Text = "Add New User";
            addNewUserToolStripMenuItem.Click += addNewUserToolStripMenuItem_Click;
            // 
            // userDetailsToolStripMenuItem
            // 
            userDetailsToolStripMenuItem.Name = "userDetailsToolStripMenuItem";
            userDetailsToolStripMenuItem.Size = new Size(288, 40);
            userDetailsToolStripMenuItem.Text = "User Details";
            userDetailsToolStripMenuItem.Click += userDetailsToolStripMenuItem_Click;
            // 
            // permissionsToolStripMenuItem
            // 
            permissionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { listPermissionsToolStripMenuItem, addNewPermissionToolStripMenuItem });
            permissionsToolStripMenuItem.Name = "permissionsToolStripMenuItem";
            permissionsToolStripMenuItem.Size = new Size(190, 40);
            permissionsToolStripMenuItem.Text = "Permissions";
            // 
            // listPermissionsToolStripMenuItem
            // 
            listPermissionsToolStripMenuItem.Name = "listPermissionsToolStripMenuItem";
            listPermissionsToolStripMenuItem.Size = new Size(376, 40);
            listPermissionsToolStripMenuItem.Text = "List Permissions";
            listPermissionsToolStripMenuItem.Click += listPermissionsToolStripMenuItem_Click;
            // 
            // addNewPermissionToolStripMenuItem
            // 
            addNewPermissionToolStripMenuItem.Name = "addNewPermissionToolStripMenuItem";
            addNewPermissionToolStripMenuItem.Size = new Size(376, 40);
            addNewPermissionToolStripMenuItem.Text = "Add New Permission";
            addNewPermissionToolStripMenuItem.Click += addNewPermissionToolStripMenuItem_Click;
            // 
            // rolesToolStripMenuItem
            // 
            rolesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { listRolesToolStripMenuItem, addNewRoleToolStripMenuItem, roleDetailsToolStripMenuItem });
            rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            rolesToolStripMenuItem.Size = new Size(101, 40);
            rolesToolStripMenuItem.Text = "Roles";
            // 
            // listRolesToolStripMenuItem
            // 
            listRolesToolStripMenuItem.Name = "listRolesToolStripMenuItem";
            listRolesToolStripMenuItem.Size = new Size(287, 40);
            listRolesToolStripMenuItem.Text = "List Roles";
            listRolesToolStripMenuItem.Click += listRolesToolStripMenuItem_Click;
            // 
            // addNewRoleToolStripMenuItem
            // 
            addNewRoleToolStripMenuItem.Name = "addNewRoleToolStripMenuItem";
            addNewRoleToolStripMenuItem.Size = new Size(287, 40);
            addNewRoleToolStripMenuItem.Text = "Add New Role";
            addNewRoleToolStripMenuItem.Click += addNewRoleToolStripMenuItem_Click;
            // 
            // roleDetailsToolStripMenuItem
            // 
            roleDetailsToolStripMenuItem.Name = "roleDetailsToolStripMenuItem";
            roleDetailsToolStripMenuItem.Size = new Size(287, 40);
            roleDetailsToolStripMenuItem.Text = "Role Details";
            roleDetailsToolStripMenuItem.Click += roleDetailsToolStripMenuItem_Click;
            // 
            // loginHistoryToolStripMenuItem
            // 
            loginHistoryToolStripMenuItem.Name = "loginHistoryToolStripMenuItem";
            loginHistoryToolStripMenuItem.Size = new Size(209, 40);
            loginHistoryToolStripMenuItem.Text = "LoginHistory";
            loginHistoryToolStripMenuItem.Click += loginHistoryToolStripMenuItem_Click;
            // 
            // auditLogToolStripMenuItem
            // 
            auditLogToolStripMenuItem.Name = "auditLogToolStripMenuItem";
            auditLogToolStripMenuItem.Size = new Size(159, 40);
            auditLogToolStripMenuItem.Text = "AuditLog";
            auditLogToolStripMenuItem.Click += auditLogToolStripMenuItem_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1139, 575);
            Controls.Add(btnLogin);
            Controls.Add(msMain);
            MainMenuStrip = msMain;
            Name = "frmMain";
            Text = "Main";
            Load += frmMain_Load;
            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnLogin;
        private MenuStrip msMain;
        private ToolStripMenuItem usersToolStripMenuItem;
        private ToolStripMenuItem listUsersToolStripMenuItem;
        private ToolStripMenuItem addNewUserToolStripMenuItem;
        private ToolStripMenuItem permissionsToolStripMenuItem;
        private ToolStripMenuItem rolesToolStripMenuItem;
        private ToolStripMenuItem loginHistoryToolStripMenuItem;
        private ToolStripMenuItem auditLogToolStripMenuItem;
        private ToolStripMenuItem listPermissionsToolStripMenuItem;
        private ToolStripMenuItem addNewPermissionToolStripMenuItem;
        private ToolStripMenuItem listRolesToolStripMenuItem;
        private ToolStripMenuItem addNewRoleToolStripMenuItem;
        private ToolStripMenuItem userDetailsToolStripMenuItem;
        private ToolStripMenuItem roleDetailsToolStripMenuItem;
    }
}
