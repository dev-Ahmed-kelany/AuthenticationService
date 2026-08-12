using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Permissions;

namespace AuthenticationService.Repository
{
   public class PermissionRepository
    {
        public static async Task<int> AddAsync(CreatePermissionDto permission)
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

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    return (int)command.Parameters["@NewPermissionID"].Value;

                }
            }
        }

        public static async Task<bool> UpdateByIDAsync(int id, UpdatePermissionDto permission)
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

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    return (int)command.Parameters["@RowsAffected"].Value == 1;

                }
            }
        }

        public static async Task<List<PermissionDetailsDto>> SearchByNameAsync(string searchText)
        {
            List<PermissionDetailsDto> permissions = new List<PermissionDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchPermissionsByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public static async Task<PermissionDetailsDto?> GetByIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPermissionByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
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

        public static async Task<List<PermissionDetailsDto>> GetAllAsync()
        {
            List<PermissionDetailsDto> permissions = new List<PermissionDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllPermissions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public static async Task<bool> ExistsAsync(int permissionID)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_PermissionExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PermissionID", permissionID);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                   
                }
            }

            return exists;
        }

        public static async Task<bool> PermissionNameExistsAsync(string permissionName)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_PermissionNameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PermissionName", permissionName);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                   
                }
            }

            return exists;
        }

        public static async Task<bool> BitValueExistsAsync(long bitValue)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_BitValueExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BitValue", bitValue);
                    
                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                    
                }
            }

            return exists;
        }

        public static async Task<PermissionDetailsDto?> GetByNameAsync(string permissionName)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPermissionByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", permissionName);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
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
    }
}
