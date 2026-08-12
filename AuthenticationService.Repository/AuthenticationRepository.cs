using AuthenticationService.Dtos.Authentication;
using AuthenticationService.Dtos.Users;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AuthenticationService.Repository
{
    public class AuthenticationRepository
    {
        public static async Task<AuthenticationUserDto?> GetAuthenticationUserByUsernameAsync(string username)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAuthenticationUserByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new AuthenticationUserDto()
                            {
                                ID = (int)reader["ID"],
                                Username = (string)reader["Username"],
                                PasswordHash = (string)reader["PasswordHash"],
                                RoleName = (string)reader["RoleName"],
                                PermissionsMask = (long)reader["PermissionsMask"],
                                StatusID = (int)reader["StatusID"]
                            };
                                
                        }
                    }
                    
                }
            }

            return null;
        }

        public static async Task<AuthenticatedUserDto?> GetAuthenticatedUserByIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAuthenticatedUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", id);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new AuthenticatedUserDto
                            {
                                ID = (int)reader["ID"],
                                Username = (string)reader["Username"],
                                RoleName = (string)reader["RoleName"],
                                PermissionsMask = (long)reader["PermissionsMask"],
                                StatusID = (int)reader["StatusID"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static async Task<bool> ChangePasswordAsync(int userId, string newPasswordHash)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_ChangePassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);

                    SqlParameter rowsAffectedParameter = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(rowsAffectedParameter);

                    try
                    {
                        await connection.OpenAsync();

                        await command.ExecuteNonQueryAsync();

                        rowsAffected = (int)rowsAffectedParameter.Value;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

    }
}
