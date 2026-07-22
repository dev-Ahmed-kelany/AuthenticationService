using AuthenticationService.WinForms.Global;

namespace AuthenticationService.WinForms.API
{
    public static class clsAuthenticationAPI
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(clsSettings.APIBaseURL)
        };

        public static async Task<bool> LoginAsync(string Username, string Password)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsync(
                    $"Auth/Login?Username={Uri.EscapeDataString(Username)}&Password={Uri.EscapeDataString(Password)}",
                    null);

                return Response.IsSuccessStatusCode;
            }
            catch
            {
            }

            return false;
        }

        public static async Task<bool> VerifyCredentialsAsync(string Username, string Password)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsync(
                    $"Auth/Verify-Credentials?Username={Uri.EscapeDataString(Username)}&Password={Uri.EscapeDataString(Password)}",
                    null);

                return Response.IsSuccessStatusCode;
            }
            catch
            {
            }

            return false;
        }

        public static async Task<bool> ChangePasswordAsync(string Username, string CurrentPassword, string NewPassword)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsync(
                    $"Auth/Change-Password?Username={Uri.EscapeDataString(Username)}&CurrentPassword={Uri.EscapeDataString(CurrentPassword)}&NewPassword={Uri.EscapeDataString(NewPassword)}",
                    null);

                return Response.IsSuccessStatusCode;
            }
            catch
            {
            }

            return false;
        }
    }
}