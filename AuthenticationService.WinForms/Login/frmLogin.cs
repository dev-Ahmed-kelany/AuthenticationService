using AuthenticationService.Dtos.Authentication;
using AuthenticationService.WinForms.Global;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AuthenticationService.WinForms.Login
{
    public partial class frmLogin : Form
    {
        // Colors used by the screen
        private readonly Color _primaryBlue =
            Color.FromArgb(37, 99, 235);

        private readonly Color _hoverBlue =
            Color.FromArgb(29, 78, 216);

        private readonly Color _darkBlue =
            Color.FromArgb(30, 58, 95);

        private readonly Color _gray =
            Color.FromArgb(209, 213, 219);

        private readonly Color _dangerRed =
            Color.FromArgb(220, 38, 38);

        public frmLogin()
        {
            InitializeComponent();

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            btnLogin.Click += btnLogin_Click;

            txtUsername.KeyDown += txtUsername_KeyDown;
            txtPassword.KeyDown += txtPassword_KeyDown;

            btnLogin.MouseEnter += btnLogin_MouseEnter;
            btnLogin.MouseLeave += btnLogin_MouseLeave;

            txtUsername.Enter += TextBox_Enter;
            txtUsername.Leave += TextBox_Leave;

            txtPassword.Enter += TextBox_Enter;
            txtPassword.Leave += TextBox_Leave;
        }

        private async void btnLogin_Click(
            object? sender,
            EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowValidationError(
                    "Username is required.");

                txtUsername.Focus();

                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowValidationError(
                    "Password is required.");

                txtPassword.Focus();

                return;
            }

            ClearValidationError();

            frmMain main = new frmMain(this);

            var loginRequest = new AuthenticationRequestDto { Username = username, Password = password };

            var loginResult = await Services.AuthenticationService.LoginAsync(loginRequest);

            if (loginResult.IsSuccess)
            {
                Session.Start(loginResult.Data);

                this.Hide();
                main.Show();
            }
            else
            {
                MessageBox.Show(loginResult.Error.Description.ToString());
            }

            
        }

        private void txtUsername_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();

                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();

                e.SuppressKeyPress = true;
            }
        }

        private void btnLogin_MouseEnter(
            object? sender,
            EventArgs e)
        {
            btnLogin.BackColor = _hoverBlue;
        }

        private void btnLogin_MouseLeave(
            object? sender,
            EventArgs e)
        {
            btnLogin.BackColor = _primaryBlue;
        }

        private void TextBox_Enter(
            object? sender,
            EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.BackColor =
                    Color.FromArgb(249, 250, 251);
            }
        }

        private void TextBox_Leave(
            object? sender,
            EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.BackColor = Color.White;
            }
        }

        private void ShowValidationError(
            string message)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = _dangerRed;
        }

        private void ClearValidationError()
        {
            lblStatus.Text = "Ready";
            lblStatus.ForeColor =
                Color.FromArgb(107, 114, 128);
        }
    }
}