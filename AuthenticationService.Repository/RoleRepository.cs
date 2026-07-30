using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Roles;

namespace AuthenticationService.Repository
{
    public class RoleRepository
    {
        public static async Task<int> AddAsync(SaveRoleDto role)
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

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    return (int)command.Parameters["@NewRoleID"].Value;

                }
            }
        }

        public static async Task<bool> UpdateByIDAsync(int id, SaveRoleDto role)
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

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    return (int)command.Parameters["@RowsAffected"].Value == 1;

                }
            }
        }

        public static async Task<List<RoleDetailsDto>> SearchByNameAsync(string searchText)
        {
            List<RoleDetailsDto> roles = new List<RoleDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchRolesByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public static async Task<RoleDetailsDto?> GetByIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoleByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
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

        public static async Task<List<RoleDetailsDto>> GetAllAsync()
        {
            List<RoleDetailsDto> roles = new List<RoleDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public static async Task<bool> ExistsAsync(int roleId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_RoleExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleID", roleId);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                   
                }
            }

            return exists;
        }

        public static async Task<bool> RoleNameExistsAsync(string roleName)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_RoleNameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleName", roleName);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                    
                }
            }

            return exists;
        }

        public static async Task<bool> RoleNameExistsAsync(string roleName, int excludeRoleId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_RoleNameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleName", roleName);
                    command.Parameters.AddWithValue("@ExcludeRoleID", excludeRoleId);

                    await connection.OpenAsync();

                    exists = Convert.ToBoolean(await command.ExecuteScalarAsync());
                }
            }

            return exists;
        }

    }
}
