using AuthenticationService.WinForms.Models;
using AuthenticationService.WinForms.Global;
using System.Net.Http.Json;

namespace AuthenticationService.WinForms.API
{
    public static class clsAuditLogAPI
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(clsSettings.APIBaseURL)
        };

        public static async Task<int> AddNewAuditLogAsync(AuditLogDTO AuditLog)
        {
            try
            {
                HttpResponseMessage Response = await _Client.PostAsJsonAsync("AuditLogs", AuditLog);

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

        public static async Task<AuditLogDTO?> GetAuditLogByIDAsync(int ID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"AuditLogs/{ID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<AuditLogDTO>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<AuditLogDTO>?> GetAllAuditLogsAsync()
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync("AuditLogs");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<AuditLogDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<AuditLogDTO>?> GetAuditLogsByUserIDAsync(int UserID)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"AuditLogs/User/{UserID}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<AuditLogDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<AuditLogDTO>?> SearchAuditLogsAsync(string SearchText)
        {
            try
            {
                HttpResponseMessage Response = await _Client.GetAsync($"AuditLogs/Search?SearchText={Uri.EscapeDataString(SearchText)}");

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<AuditLogDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }

        public static async Task<List<AuditLogDTO>?> FilterAuditLogsAsync(int? EntityID, int? OperationTypeID)
        {
            try
            {
                string URL = $"AuditLogs/Filter?";

                if (EntityID.HasValue)
                    URL += $"EntityID={EntityID.Value}&";

                if (OperationTypeID.HasValue)
                    URL += $"OperationTypeID={OperationTypeID.Value}&";

                URL = URL.TrimEnd('&', '?');

                HttpResponseMessage Response = await _Client.GetAsync(URL);

                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<List<AuditLogDTO>>();
                }
            }
            catch
            {
            }

            return null;
        }
    }
}