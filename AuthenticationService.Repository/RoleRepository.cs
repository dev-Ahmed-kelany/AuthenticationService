using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Roles;

namespace AuthenticationService.Repository
{
    public class RoleRepository
    {
        public static int AddRole(SaveRoleDto role)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewRole", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@Name", role.Name);
                    command.Parameters.AddWithValue("@PermissionsMask", role.PermissionsMask);

                    //-- Add Output Parameter --//
                    SqlParameter outputNewRoleID = new SqlParameter("@NewRoleID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputNewRoleID);

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@NewRoleID"].Value;

                }
            }
        }

        public static bool UpdateRoleByID(int id, SaveRoleDto role)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateRoleByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", role.Name);
                    command.Parameters.AddWithValue("@PermissionsMask", role.PermissionsMask);

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

        public static List<RoleDetailsDto> SearchRolesByName(string searchText)
        {
            List<RoleDetailsDto> roles = new List<RoleDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchRolesByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoleDetailsDto role = new RoleDetailsDto();

                            role.ID = (int)reader["ID"];
                            role.Name = (string)reader["Name"];
                            role.PermissionsMask = (long)reader["PermissionsMask"];

                            roles.Add(role);
                        }
                    }
                }
            }

            return roles;
        }

        public static RoleDetailsDto? GetRoleByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoleByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RoleDetailsDto
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                PermissionsMask = (long)reader["PermissionsMask"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<RoleDetailsDto> GetAllRoles()
        {
            List<RoleDetailsDto> roles = new List<RoleDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoleDetailsDto role = new RoleDetailsDto();

                            role.ID = (int)reader["ID"];
                            role.Name = (string)reader["Name"];
                            role.PermissionsMask = (long)reader["PermissionsMask"];

                            roles.Add(role);
                        }
                    }
                }
            }

            return roles;
        }

    }
}
