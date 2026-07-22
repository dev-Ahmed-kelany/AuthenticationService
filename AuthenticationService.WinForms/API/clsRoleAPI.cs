using AuthenticationService.WinForms.Global;
using AuthenticationService.WinForms.Models;
using System.Net.Http.Json;

namespace AuthenticationService.WinForms.API
{
    public static class clsRoleAPI
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(clsSettings.APIBaseURL)
        };

        public static async Task<int> AddNewRoleAsync(string Name, long PermissionsMask)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsync(
                    $"Roles?Name={Uri.EscapeDataString(Name)}&PermissionsMask={PermissionsMask}", null);

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

        public static async Task<bool> UpdateRoleByIDAsync(int ID, string Name, long PermissionsMask)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PutAsync(
                    $"Roles/{ID}?Name={Uri.EscapeDataString(Name)}&PermissionsMask={PermissionsMask}", null);

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

        public static async Task<List<RoleDTO>?> SearchRolesByNameAsync(string SearchText)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync(
                    $"Roles/Search?SearchText={Uri.EscapeDataString(SearchText)}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<RoleDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<RoleDTO?> GetRoleByIDAsync(int ID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"Roles/{ID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<RoleDTO>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<RoleDTO>?> GetAllRolesAsync()
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync("Roles");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<RoleDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}