namespace AuthenticationService.WinForms.Models
{
    public class LoginHistoryDTO
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public string? Username { get; set; }

        public string? Name { get; set; }

        public byte Status { get; set; }

        public string? FailureReason { get; set; }

        public string? IPAddress { get; set; }

        public string? Device { get; set; }

        public string? Browser { get; set; }

        public DateTime DateTime { get; set; }
    }
}