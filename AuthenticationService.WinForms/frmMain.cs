using AuthenticationService.WinForms.Users;
using AuthenticationService.WinForms.Roles;
using AuthenticationService.WinForms.Permissions;
using AuthenticationService.WinForms.LoginHistory;
using AuthenticationService.WinForms.AuditLogs;
using AuthenticationService.WinForms.Login;
using AuthenticationService.WinForms.Global;

namespace AuthenticationService.WinForms
{
    public partial class frmMain : Form
    {
        frmLogin _Login;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(frmLogin login)
        {
            InitializeComponent();
            _Login = login;
        }

        private void loginHistoryToolStripMenuItem_Click(
            object? sender,
            EventArgs e)
        {
            frmLoginHistoryList frm = new frmLoginHistoryList();

            frm.ShowDialog();
        }

        private void auditLogToolStripMenuItem_Click(
            object? sender,
            EventArgs e)
        {
            frmAuditLogsList frm = new frmAuditLogsList();

            frm.ShowDialog();
        }

        private void btnLogout_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to logout?",
                    "Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result != DialogResult.Yes)
                return;

            // Clear session here.
            //
            Session.Clear();
            _Login.Show();
            this.Close();
        }

        private void usersListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersList frm = new frmUsersList();

            frm.ShowDialog();
        }

        private void permissionsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPermissionsList frm = new frmPermissionsList();

            frm.ShowDialog();
        }

        private void rolesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRolesList frm = new frmRolesList();

            frm.ShowDialog();
        }
    }
}