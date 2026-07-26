using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class ProfileDTO
    {
        public int ID { get; set; }

        public string Name { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string RoleName { get; set; } = null!;

        public string StatusName { get; set; } = null!;
    }

    public class ProfileRepository
    {
        public static ProfileDTO? GetProfile(int userId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetProfileByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProfileDTO
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
