using AuthenticationService.Dtos.AuditLogs;
using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.LoginHistory;
using AuthenticationService.WinForms.Global;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AuthenticationService.WinForms.Services
{
    public static class LoginHistoryService
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.APIBaseURL)
        };

        public static async Task<Result<LoginHistoryDetailsDto?>> GetByIDAsync(int id, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"LoginHistory/{id}");

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

                        if (!refreshResult.IsSuccess) return Result<LoginHistoryDetailsDto?>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByIDAsync(id, false);
                    }

                    return Result<LoginHistoryDetailsDto?>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<LoginHistoryDetailsDto?>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<LoginHistoryDetailsDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<LoginHistoryDetailsDto?>.Success(data);
        }

        public static async Task<Result<List<LoginHistoryDetailsDto?>>> GetAllAsync(bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"LoginHistory");

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

                        if (!refreshResult.IsSuccess) return Result<List<LoginHistoryDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetAllAsync(false);
                    }

                    return Result<List<LoginHistoryDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<LoginHistoryDetailsDto?>>.Failure(error);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<LoginHistoryDetailsDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<LoginHistoryDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<LoginHistoryDetailsDto?>>> GetByUserIDAsync(int userId, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"LoginHistory/User/{userId}");

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

                        if (!refreshResult.IsSuccess) return Result<List<LoginHistoryDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByUserIDAsync(userId, false);
                    }

                    return Result<List<LoginHistoryDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<LoginHistoryDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<LoginHistoryDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<LoginHistoryDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<LoginHistoryDetailsDto?>>> SearchAsync(string searchText, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"LoginHistory/Search?SearchText={Uri.EscapeDataString(searchText)}");

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

                        if (!refreshResult.IsSuccess) return Result<List<LoginHistoryDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await SearchAsync(searchText, false);
                    }

                    return Result<List<LoginHistoryDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<LoginHistoryDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<LoginHistoryDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<LoginHistoryDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<LoginHistoryDetailsDto?>>> FilterByStatusAsync(byte status, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"LoginHistory/Status/{status}");

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

                        if (!refreshResult.IsSuccess) return Result<List<LoginHistoryDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await FilterByStatusAsync(status, false);
                    }

                    return Result<List<LoginHistoryDetailsDto?>>.Failure(
                        new Error(
                            "Forbidden",
                            "You do not have permission to access this resource.",
                            statusCode));
                }

                var apiError =
                    JsonSerializer.Deserialize<ApiError>(
                        jsonResponseError, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var error = new Error(
                    apiError?.Code ?? "Unknown",
                    apiError?.Message ??
                        "An unexpected error occurred.",
                    statusCode);

                return Result<List<LoginHistoryDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<LoginHistoryDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<LoginHistoryDetailsDto?>>.Success(data);
        }
    }
}