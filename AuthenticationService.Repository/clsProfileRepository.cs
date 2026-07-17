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

    public class clsProfileRepository
    {
        public static ProfileDTO? GetProfile(int UserID)
        {
            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetProfileByUserID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@UserID", UserID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            return new ProfileDTO
                            {
                                ID = (int)Reader["ID"],
                                Name = (string)Reader["Name"],
                                Username = (string)Reader["Username"],
                                Email = (string)Reader["Email"],
                                RoleName = (string)Reader["RoleName"],
                                StatusName = (string)Reader["StatusName"],
                                CreatedAt = (DateTime)Reader["CreatedAt"]
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
