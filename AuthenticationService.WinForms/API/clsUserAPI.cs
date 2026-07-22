using AuthenticationService.WinForms.Global;
using AuthenticationService.WinForms.Models;
using System.Net.Http.Json;

namespace AuthenticationService.WinForms.API
{
    public static class clsUserAPI
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(clsSettings.APIBaseURL)
        };

        public static async Task<int> AddNewUserAsync(string Name, string Username,
            string Email, string PasswordHash, int RoleID, int StatusID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsync(
                    $"Users?Name={Uri.EscapeDataString(Name)}" +
                    $"&Username={Uri.EscapeDataString(Username)}" +
                    $"&Email={Uri.EscapeDataString(Email)}" +
                    $"&PasswordHash={Uri.EscapeDataString(PasswordHash)}" +
                    $"&RoleID={RoleID}" +
                    $"&StatusID={StatusID}",
                    null);

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

        public static async Task<bool> UpdateUserByIDAsync(int ID, string Name,
            string Username, string Email, int RoleID, int StatusID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PutAsync(
                    $"Users/{ID}?Name={Uri.EscapeDataString(Name)}" +
                    $"&Username={Uri.EscapeDataString(Username)}" +
                    $"&Email={Uri.EscapeDataString(Email)}" +
                    $"&RoleID={RoleID}" +
                    $"&StatusID={StatusID}",
                    null);

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

        public static async Task<bool> DeleteUserByIDAsync(int ID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.DeleteAsync($"Users/{ID}");

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

        public static async Task<List<UserDTO>?> SearchUsersAsync(string SearchText)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync(
                    $"Users/Search?SearchText={Uri.EscapeDataString(SearchText)}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<UserDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<UserDTO>?> FilterUsersByRoleIDAsync(int RoleID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"Users/Filter/Role/{RoleID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<UserDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<UserDTO>?> FilterUsersByStatusIDAsync(int StatusID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"Users/Filter/Status/{StatusID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<UserDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<UserDTO>?> GetAllUsersAsync()
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync("Users");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<UserDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<UserDTO?> GetUserByIDAsync(int ID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"Users/{ID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<UserDTO>();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}