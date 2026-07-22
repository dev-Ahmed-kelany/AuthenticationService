using AuthenticationService.WinForms.Global;
using AuthenticationService.WinForms.Models;
using System.Net.Http.Json;

namespace AuthenticationService.WinForms.API
{
    public static class clsPermissionAPI
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(clsSettings.APIBaseURL)
        };

        public static async Task<int> AddNewPermissionAsync(string Name, long BitValue)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsync(
                    $"Permissions?Name={Uri.EscapeDataString(Name)}&BitValue={BitValue}", null);

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

        public static async Task<bool> UpdatePermissionByIDAsync(int ID, string Name)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PutAsync(
                    $"Permissions/{ID}?Name={Uri.EscapeDataString(Name)}", null);

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<bool>();
                }
            }
            catch
            {
            }

            return false;
        }

        public static async Task<List<PermissionDTO>?> SearchPermissionsByNameAsync(string SearchText)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync(
                    $"Permissions/Search?SearchText={Uri.EscapeDataString(SearchText)}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<PermissionDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<PermissionDTO?> GetPermissionByIDAsync(int ID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"Permissions/{ID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<PermissionDTO>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<PermissionDTO>?> GetAllPermissionsAsync()
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync("Permissions");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<PermissionDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}