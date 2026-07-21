using AuthenticationService.WinForms.Models;
using AuthenticationService.WinForms.API;

namespace AuthenticationService.WinForms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            AuditLogDTO? auditLogDTO = await clsAuditLogAPI.GetAuditLogByIDAsync(1);

            if (auditLogDTO != null)
            {
                lblTest.Text = $"ID: {auditLogDTO.ID}, Username: {auditLogDTO.Username}, Entity Name: {auditLogDTO.EntityName}";
            }
            else lblTest.Text = "Error";
        }
    }
}
