using AuthenticationService.WinForms.Global;
using AuthenticationService.WinForms.Models;
using System.Net.Http.Json;

namespace AuthenticationService.WinForms.API
{
    public static class clsLoginHistoryAPI
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(clsSettings.APIBaseURL)
        };

        public static async Task<int> AddNewLoginHistoryAsync(LoginHistoryDTO LoginHistory)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsJsonAsync("LoginHistory", LoginHistory);

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<int>();
                }
            }
            catch
            {
            }

            return -1;
        }

        public static async Task<LoginHistoryDTO?> GetLoginHistoryByIDAsync(int ID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"LoginHistory/{ID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<LoginHistoryDTO>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<LoginHistoryDTO>?> GetAllLoginHistoryAsync()
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync("LoginHistory");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<LoginHistoryDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<LoginHistoryDTO>?> GetLoginHistoryByUserIDAsync(int UserID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"LoginHistory/User/{UserID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<LoginHistoryDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<LoginHistoryDTO>?> SearchLoginHistoryAsync(string SearchText)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"LoginHistory/Search?SearchText={Uri.EscapeDataString(SearchText)}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<LoginHistoryDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<LoginHistoryDTO>?> FilterLoginHistoryByStatusAsync(byte Status)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"LoginHistory/Status/{Status}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<LoginHistoryDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}