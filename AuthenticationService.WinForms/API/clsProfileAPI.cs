using AuthenticationService.WinForms.Global;
using AuthenticationService.WinForms.Models;
using System.Net.Http.Json;

namespace AuthenticationService.WinForms.API
{
    public static class clsProfileAPI
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(clsSettings.APIBaseURL)
        };

        public static async Task<ProfileDTO?> GetProfileByUserIDAsync(int ID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"Profile/{ID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<ProfileDTO>();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}