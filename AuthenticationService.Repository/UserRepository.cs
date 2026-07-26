using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AuthenticationService.Repository
{
    public class UserDTO
    {
        // Properties
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleID { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class UserRepository
    {
        public static int AddUser(string name, string username, string email,
            string passwordHash, int roleId, int statusId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@RoleID", roleId);
                    command.Parameters.AddWithValue("@StatusID", statusId);

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

        public static bool UpdateUserByID(int id, string name, string username, string email
            , int roleId, int statusId)
        {

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@UserID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@RoleID", roleId);
                    command.Parameters.AddWithValue("@StatusID", statusId);

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

        public static List<UserDTO> SearchUsers(string searchText)
        {
            List<UserDTO> users = new List<UserDTO>();

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
                            UserDTO user = new UserDTO();

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

        public static List<UserDTO> FilterUsersByRoleID(int roleId)
        {
            List<UserDTO> users = new List<UserDTO>();

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
                            UserDTO user = new UserDTO();

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

        public static List<UserDTO> FilterUsersByStatusID(int statusId)
        {
            List<UserDTO> users = new List<UserDTO>();

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
                            UserDTO user = new UserDTO();

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

        public static List<UserDTO> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();

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
                            UserDTO user = new UserDTO();

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

        public static UserDTO? GetUserByID(int id)
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
                            return new UserDTO
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

    }
}
