using AuthenticationService.Dtos.AuditLogs;
using AuthenticationService.Dtos.Authentication;
using AuthenticationService.WinForms.Global;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AuthenticationService.WinForms.Services
{
    public static class AuditLogService
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.APIBaseURL)
        };

        public static async Task<Result<AuditLogDetailsDto?>> GetByIDAsync(int id, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"AuditLogs/{id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            HttpResponseMessage response = await _Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError =
                    await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResponseError))
                {
                    if (statusCode == 401 && retryAfterRefresh)
                    {
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken });

                        if (!refreshResult.IsSuccess) return Result<AuditLogDetailsDto>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByIDAsync(id, false);
                    }

                    return Result<AuditLogDetailsDto?>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError);

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<AuditLogDetailsDto?>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<AuditLogDetailsDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<AuditLogDetailsDto?>.Success(data);
        }

        public static async Task<Result<List<AuditLogDetailsDto?>>> GetAllAsync(bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"AuditLogs");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            HttpResponseMessage response = await _Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError =
                    await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResponseError))
                {
                    if (statusCode == 401 && retryAfterRefresh)
                    {
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken });

                        if (!refreshResult.IsSuccess) return Result<List<AuditLogDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetAllAsync(false);
                    }

                    return Result<List<AuditLogDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError);

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<AuditLogDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<AuditLogDetailsDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<AuditLogDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<AuditLogDetailsDto?>>> GetByUserIDAsync(int userId, bool retryAfterRefresh)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"AuditLogs/User/{userId}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            HttpResponseMessage response = await _Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError =
                    await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResponseError))
                {
                    if (statusCode == 401 && retryAfterRefresh)
                    {
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken });

                        if (!refreshResult.IsSuccess) return Result<List<AuditLogDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByUserIDAsync(userId, false);
                    }

                    return Result<List<AuditLogDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError);

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<AuditLogDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<AuditLogDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<AuditLogDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<AuditLogDetailsDto?>>> SearchAsync(string searchText, bool retryAfterRefresh)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"AuditLogs/Search?SearchText={Uri.EscapeDataString(searchText)}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            HttpResponseMessage response = await _Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError =
                    await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResponseError))
                {
                    if (statusCode == 401 && retryAfterRefresh)
                    {
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken });

                        if (!refreshResult.IsSuccess) return Result<List<AuditLogDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await SearchAsync(searchText, false);
                    }

                    return Result<List<AuditLogDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError);

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<AuditLogDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<AuditLogDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<AuditLogDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<AuditLogDetailsDto?>>> FilterAsync(int? entityId, int? operationTypeId, bool retryAfterRefresh)
        {
            string URL = $"AuditLogs/Filter?";

            if (entityId.HasValue)
                URL += $"EntityID={entityId.Value}&";

            if (operationTypeId.HasValue)
                URL += $"OperationTypeID={operationTypeId.Value}&";

            URL = URL.TrimEnd('&', '?');

            var request = new HttpRequestMessage(HttpMethod.Get, URL);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            HttpResponseMessage response = await _Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                var jsonResponseError =
                    await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResponseError))
                {
                    if (statusCode == 401 && retryAfterRefresh)
                    {
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken });

                        if (!refreshResult.IsSuccess) return Result<List<AuditLogDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await FilterAsync(entityId, operationTypeId,false);
                    }

                    return Result<List<AuditLogDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError);

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<AuditLogDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<AuditLogDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<AuditLogDetailsDto?>>.Success(data);
        }
    }
}