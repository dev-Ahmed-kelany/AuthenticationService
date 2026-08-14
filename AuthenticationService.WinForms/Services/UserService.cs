using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.Users;
using AuthenticationService.WinForms.Global;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AuthenticationService.WinForms.Services
{
    public static class UserService
    {
        private static readonly HttpClient _Client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.APIBaseURL)
        };

        public static async Task<Result<int>> AddAsync(CreateUserDto user, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"Users");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            var jsonRequest = JsonSerializer.Serialize(user);
            request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

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
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken ?? "" });

                        if (!refreshResult.IsSuccess) return Result<int>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await AddAsync(user, false);
                    }

                    return Result<int>.Failure(
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

                return Result<int>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<int>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<int>.Success(data);
        }

        public static async Task<Result<bool>> UpdateByIDAsync(int id, UpdateUserDto user, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"Users/{id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session.AccessToken);

            var jsonRequest = JsonSerializer.Serialize(user);
            request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

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
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken ?? "" });

                        if (!refreshResult.IsSuccess) return Result<bool>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await UpdateByIDAsync(id, user, false);
                    }

                    return Result<bool>.Failure(
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

                return Result<bool>.Failure(error);
            }

            return Result<bool>.Success(true);
        }

        public static async Task<Result<bool>> DeleteByIDAsync(int id, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"Users/{id}");

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
                        var refreshResult = await AuthenticationService.RefreshAsync(new RefreshTokenRequestDto { RefreshToken = Session.RefreshToken ?? "" });

                        if (!refreshResult.IsSuccess) return Result<bool>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await DeleteByIDAsync(id, false);
                    }

                    return Result<bool>.Failure(
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

                return Result<bool>.Failure(error);
            }

            return Result<bool>.Success(true);
        }

        public static async Task<Result<List<UserDetailsDto?>>> SearchAsync(string searchText, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Users/Search?SearchText={Uri.EscapeDataString(searchText)}");

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

                        if (!refreshResult.IsSuccess) return Result<List<UserDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await SearchAsync(searchText, false);
                    }

                    return Result<List<UserDetailsDto?>>.Failure(
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

                return Result<List<UserDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<UserDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<UserDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<UserDetailsDto?>>> FilterByRoleIDAsync(int roleId, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Users/Filter/Role/{roleId}");

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

                        if (!refreshResult.IsSuccess) return Result<List<UserDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await FilterByRoleIDAsync(roleId, false);
                    }

                    return Result<List<UserDetailsDto?>>.Failure(
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

                return Result<List<UserDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<UserDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<UserDetailsDto?>>.Success(data);

        }

        public static async Task<Result<List<UserDetailsDto?>>> FilterByStatusIDAsync(int statusId, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Users/Filter/Status/{statusId}");

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

                        if (!refreshResult.IsSuccess) return Result<List<UserDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await FilterByStatusIDAsync(statusId, false);
                    }

                    return Result<List<UserDetailsDto?>>.Failure(
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

                return Result<List<UserDetailsDto?>>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<UserDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<UserDetailsDto?>>.Success(data);
        }

        public static async Task<Result<List<UserDetailsDto?>>> GetAllAsync(bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Users");

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

                        if (!refreshResult.IsSuccess) return Result<List<UserDetailsDto?>>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetAllAsync(false);
                    }

                    return Result<List<UserDetailsDto?>>.Failure(
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

                return Result<List<UserDetailsDto?>>.Failure(error);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<UserDetailsDto?>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<List<UserDetailsDto?>>.Success(data);
        }

        public static async Task<Result<UserDetailsDto?>> GetByIDAsync(int id, bool retryAfterRefresh = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Users/{id}");

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

                        if (!refreshResult.IsSuccess) return Result<UserDetailsDto?>.Failure(refreshResult.Error);

                        Session.RefreshTokens(refreshResult.Data);

                        return await GetByIDAsync(id, false);
                    }

                    return Result<UserDetailsDto?>.Failure(
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

                return Result<UserDetailsDto?>.Failure(error);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<UserDetailsDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Result<UserDetailsDto?>.Success(data);
        }
    }
}