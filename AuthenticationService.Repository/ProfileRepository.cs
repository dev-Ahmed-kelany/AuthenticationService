using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Profile;

namespace AuthenticationService.Repository
{
    
    public class ProfileRepository
    {
        public static async Task<ProfileDetailsDto?> GetProfileAsync(int userId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetProfileByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new ProfileDetailsDto
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                Username = (string)reader["Username"],
                                Email = (string)reader["Email"],
                                RoleName = (string)reader["RoleName"],
                                StatusName = (string)reader["StatusName"],
                                CreatedAt = (DateTime)reader["CreatedAt"]
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
