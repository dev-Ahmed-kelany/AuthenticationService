using AuthenticationService.Dtos.Authentication;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AuthenticationService.Repository
{
    public static class RefreshTokenRepository
    {
        public static async Task<int> CreateAsync(
            string tokenHash,
            int userId,
            DateTime expiresAt)
        {
            using (SqlConnection connection =
                new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("SP_CreateRefreshToken", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@TokenHash", SqlDbType.VarBinary, 32)
                        .Value = Convert.FromHexString(tokenHash);

                    command.Parameters.Add("@UserID", SqlDbType.Int)
                        .Value = userId;

                    command.Parameters.Add("@ExpiresAt", SqlDbType.DateTime)
                        .Value = expiresAt;

                    await connection.OpenAsync();

                    object? result = await command.ExecuteScalarAsync();

                    return Convert.ToInt32(result);
                }
            }
        }

        public static async Task<RefreshTokenDto?> GetByHashAsync(
            string tokenHash)
        {
            using (SqlConnection connection =
                new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("SP_GetRefreshTokenByHash", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@TokenHash", SqlDbType.VarBinary, 32)
                        .Value = Convert.FromHexString(tokenHash);

                    await connection.OpenAsync();

                    using (SqlDataReader reader =
                        await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new RefreshTokenDto
                            {
                                ID = (int)reader["ID"],
                                TokenHash = Convert.ToHexString(
                                    (byte[])reader["TokenHash"]),
                                UserID = (int)reader["UserID"],
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                ExpiresAt = (DateTime)reader["ExpiresAt"],
                                RevokedAt = reader["RevokedAt"] == DBNull.Value
                                    ? null
                                    : (DateTime)reader["RevokedAt"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static async Task<bool> RevokeAsync(int id)
        {
            using (SqlConnection connection =
                new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("SP_RevokeRefreshToken", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int)
                        .Value = id;

                    await connection.OpenAsync();

                    object? result = await command.ExecuteScalarAsync();

                    return Convert.ToInt32(result) > 0;
                }
            }
        }
    }
}