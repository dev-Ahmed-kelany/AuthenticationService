using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Users;

namespace AuthenticationService.Repository
{
    public class UserRepository
    {
        public static int AddUser(CreateUserDto user)
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

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@NewUserID"].Value;

                }
            }
        }

        public static bool UpdateUserByID(int id, UpdateUserDto user)
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

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@RowsAffected"].Value == 1;
                }
            }
        }

        public static bool DeleteUserByID(int id)
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

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)outputRowsAffected.Value == 1;
                }
            }
        }

        public static List<UserDetailsDto> SearchUsers(string searchText)
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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

        public static List<UserDetailsDto> FilterUsersByRoleID(int roleId)
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FilterUsersByRoleID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleID", roleId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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

        public static List<UserDetailsDto> FilterUsersByStatusID(int statusId)
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FilterUsersByStatusID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StatusID", statusId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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

        public static List<UserDetailsDto> GetAllUsers()
        {
            List<UserDetailsDto> users = new List<UserDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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

        public static UserDetailsDto? GetUserByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
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

        public static bool UserExists(int userId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UserExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        connection.Open();

                        exists = Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception)
                    {
                        // Error Handling will be added later.
                    }
                }
            }

            return exists;
        }

        public static bool UsernameExists(string username)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UsernameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();

                        exists = Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception)
                    {
                        // Error Handling will be added later.
                    }
                }
            }

            return exists;
        }
        public static bool UsernameExists(string username, int excludeUserId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UsernameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@ExcludeUserID", excludeUserId);


                    try
                    {
                        connection.Open();

                        exists = Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception)
                    {
                        // Error Handling will be added later.
                    }
                }
            }

            return exists;
        }
        public static bool EmailExists(string email)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_EmailExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        connection.Open();

                        exists = Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception)
                    {
                        // Error Handling will be added later.
                    }
                }
            }

            return exists;
        }
        public static bool EmailExists(string email, int excludeUserId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_EmailExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@ExcludeUserID", excludeUserId);

                    try
                    {
                        connection.Open();

                        exists = Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception)
                    {
                        // Error Handling will be added later.
                    }
                }
            }

            return exists;
        }

    }
}
