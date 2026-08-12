using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Users;

namespace AuthenticationService.Repository
{
    public class UserRepository
    {
        public static async Task<int> AddAsync(CreateUserDto user)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@PasswordHash", user.Password);
                    command.Parameters.AddWithValue("@RoleID", user.RoleID);
                    command.Parameters.AddWithValue("@StatusID", user.StatusID);

                    //-- Add Output Parameter --//
                    SqlParameter outputNewUserID = new SqlParameter("@NewUserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputNewUserID);

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    return (int)command.Parameters["@NewUserID"].Value;

                }
            }
        }

        public static async Task<bool> UpdateByIDAsync(int id, UpdateUserDto user)
        {

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@UserID", id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@RoleID", user.RoleID);
                    command.Parameters.AddWithValue("@StatusID", user.StatusID);

                    //-- Add Output Parameter --//
                    SqlParameter outputRowsAffected = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputRowsAffected);

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    return (int)command.Parameters["@RowsAffected"].Value == 1;
                }
            }
        }

        public static async Task<bool> DeleteByIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Input Parameter --//
                    command.Parameters.AddWithValue("@UserID", id);

                    //-- Output Parameter --//
                    SqlParameter outputRowsAffected = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputRowsAffected);

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    return (int)outputRowsAffected.Value == 1;
                }
            }
        }

        public static async Task<List<UserDetailsDto>> SearchAsync(string searchText)
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserDetailsDto user = new UserDetailsDto();

                            user.ID = (int)reader["ID"];
                            user.Name = (string)reader["Name"];
                            user.Username = (string)reader["Username"];
                            user.Email = (string)reader["Email"];
                            user.RoleID = (int)reader["RoleID"];
                            user.StatusID = (int)reader["StatusID"];
                            user.CreatedAt = (DateTime)reader["CreatedAt"];

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public static async Task<List<UserDetailsDto>> FilterByRoleIDAsync(int roleId)
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FilterUsersByRoleID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleID", roleId);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserDetailsDto user = new UserDetailsDto();

                            user.ID = (int)reader["ID"];
                            user.Name = (string)reader["Name"];
                            user.Username = (string)reader["Username"];
                            user.Email = (string)reader["Email"];
                            user.RoleID = (int)reader["RoleID"];
                            user.StatusID = (int)reader["StatusID"];
                            user.CreatedAt = (DateTime)reader["CreatedAt"];

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public static async Task<List<UserDetailsDto>> FilterByStatusIDAsync(int statusId)
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FilterUsersByStatusID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StatusID", statusId);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserDetailsDto user = new UserDetailsDto();

                            user.ID = (int)reader["ID"];
                            user.Name = (string)reader["Name"];
                            user.Username = (string)reader["Username"];
                            user.Email = (string)reader["Email"];
                            user.RoleID = (int)reader["RoleID"];
                            user.StatusID = (int)reader["StatusID"];
                            user.CreatedAt = (DateTime)reader["CreatedAt"];

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public static async Task<List<UserDetailsDto>> GetAllAsync()
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserDetailsDto user = new UserDetailsDto();

                            user.ID = (int)reader["ID"];
                            user.Name = (string)reader["Name"];
                            user.Username = (string)reader["Username"];
                            user.Email = (string)reader["Email"];
                            user.RoleID = (int)reader["RoleID"];
                            user.StatusID = (int)reader["StatusID"];
                            user.CreatedAt = (DateTime)reader["CreatedAt"];

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public static async Task<UserDetailsDto?> GetByIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", id);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UserDetailsDto
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                Username = (string)reader["Username"],
                                Email = (string)reader["Email"],
                                RoleID = (int)reader["RoleID"],
                                StatusID = (int)reader["StatusID"],
                                CreatedAt = (DateTime)reader["CreatedAt"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static async Task<bool> ExistsAsync(int userId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UserExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                    
                }
            }

            return exists;
        }

        public static async Task<bool> UsernameExistsAsync(string username)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UsernameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                }
            }

            return exists;
        }
        public static async Task<bool> UsernameExistsAsync(string username, int excludeUserId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UsernameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@ExcludeUserID", excludeUserId);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                    
                }
            }

            return exists;
        }

        public static async Task<bool> EmailExistsAsync(string email)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_EmailExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);
                    
                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                   
                }
            }

            return exists;
        }
        public static async Task<bool> EmailExistsAsync(string email, int excludeUserId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_EmailExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@ExcludeUserID", excludeUserId);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                   
                }
            }

            return exists;
        }

    }
}
