using AuthenticationService.WinForms.Models;
using AuthenticationService.WinForms.API;
using AuthenticationService.WinForms.Login;
using AuthenticationService.WinForms.Global;
using AuthenticationService.WinForms.Users;
using AuthenticationService.WinForms.Permissions;
using AuthenticationService.WinForms.Roles;
using AuthenticationService.WinForms.LoginHistory;
using AuthenticationService.WinForms.AuditLogs;

namespace AuthenticationService.WinForms
{
    public partial class frmMain : Form
    {
        frmLogin _Login;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(frmLogin Login)
        {
            InitializeComponent();

            _Login = Login;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            _Login.Show();
        }

        private void listUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersList UsersList = new frmUsersList();

            UsersList.Show();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser AddUser = new frmAddEditUser();

            AddUser.Show();
        }

        private void userDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails UserDetails = new frmUserDetails();

            UserDetails.Show();
        }

        private void listPermissionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPermissionsList PermissionsList = new frmPermissionsList();

            PermissionsList.Show();
        }

        private void addNewPermissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPermission AddPermission = new frmAddEditPermission();

            AddPermission.Show();
        }

        private void listRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRolesList RolesList = new frmRolesList();

            RolesList.Show();
        }

        private void addNewRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditRole AddRole = new frmAddEditRole();

            AddRole.Show();
        }

        private void roleDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoleDetails RoleDetails = new frmRoleDetails();

            RoleDetails.Show();
        }

        private void loginHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginHistoryList LoginHistoryList = new frmLoginHistoryList();

            LoginHistoryList.Show();
        }

        private void auditLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuditLogsList AuditLogsList = new frmAuditLogsList();

            AuditLogsList.Show();
        }
    }
}
