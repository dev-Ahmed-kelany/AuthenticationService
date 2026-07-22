using AuthenticationService.WinForms.Models;
using AuthenticationService.WinForms.API;
using AuthenticationService.WinForms.Login;
using AuthenticationService.WinForms.Global;

namespace AuthenticationService.WinForms
{
    public partial class frmMain : Form
    {
        frmLogin _Login;

        public frmMain(frmLogin Login)
        {
            InitializeComponent();

            _Login = Login;
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            lblTest.Text = $"ID: {clsGlobal.CurrentUser.ID}, Username: {clsGlobal.CurrentUser.Username}, Email: {clsGlobal.CurrentUser.Email}";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            _Login.Show();
        }
    }
}
