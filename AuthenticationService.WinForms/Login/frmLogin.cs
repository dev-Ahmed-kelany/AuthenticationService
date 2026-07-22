using AuthenticationService.WinForms.API;
using AuthenticationService.WinForms.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthenticationService.WinForms.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            bool IsLoggedIn = await clsAuthenticationAPI.LoginAsync(tbUsername.Text, tbPassword.Text);

            if (IsLoggedIn)
            {
                frmMain Main = new frmMain(this);

                clsGlobal.CurrentUser = (await clsUserAPI.SearchUsersAsync(tbUsername.Text))?.FirstOrDefault();

                this.Hide();
                Main.Show();
            }
        }
    }
}
