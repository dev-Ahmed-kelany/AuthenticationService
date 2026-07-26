using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Permissions;

namespace AuthenticationService.Repository
{
   public class PermissionRepository
    {
        public static int AddPermission(CreatePermissionDto permission)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewPermission", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@Name", permission.Name);
                    command.Parameters.AddWithValue("@BitValue", permission.BitValue);

                    //-- Add Output Parameter --//
                    SqlParameter outputNewPermissionID = new SqlParameter("@NewPermissionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputNewPermissionID);

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@NewPermissionID"].Value;

                }
            }
        }

        public static bool UpdatePermissionByID(int id, UpdatePermissionDto permission)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdatePermissionByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", permission.Name);

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

        public static List<PermissionDetailsDto> SearchPermissionsByName(string searchText)
        {
            List<PermissionDetailsDto> permissions = new List<PermissionDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchPermissionsByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PermissionDetailsDto permission = new PermissionDetailsDto();

                            permission.ID = (int)reader["ID"];
                            permission.Name = (string)reader["Name"];
                            permission.BitValue = (long)reader["BitValue"];

                            permissions.Add(permission);
                        }
                    }
                }
            }

            return permissions;
        }

        public static PermissionDetailsDto? GetPermissionByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPermissionByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PermissionDetailsDto
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                BitValue = (long)reader["BitValue"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<PermissionDetailsDto> GetAllPermissions()
        {
            List<PermissionDetailsDto> permissions = new List<PermissionDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllPermissions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PermissionDetailsDto permission = new PermissionDetailsDto();

                            permission.ID = (int)reader["ID"];
                            permission.Name = (string)reader["Name"];
                            permission.BitValue = (long)reader["BitValue"];

                            permissions.Add(permission);
                        }
                    }
                }
            }

            return permissions;
        }
    }
}
