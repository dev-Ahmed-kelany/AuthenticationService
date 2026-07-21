namespace AuthenticationService.WinForms.Models
{
    public class LoginHistoryDTO
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string IPAddress { get; set; } = string.Empty;

        public string Device { get; set; } = string.Empty;

        public string Browser { get; set; } = string.Empty;

        public byte Status { get; set; }

        public string FailureReason { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }
    }
}