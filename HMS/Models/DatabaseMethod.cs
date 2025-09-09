using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.Models
{
    public class DatabaseMethod
    {
        private readonly string connection;

        public DatabaseMethod(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("ConnectionString");
        }

        public User? check_Login(string email, string password)
        {
            User? user = null;
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SP_User_Login", con))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", email);
                    sqlCommand.Parameters.AddWithValue("@Password", password);

                    con.Open();

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            user = new User
                            {
                                UserId = Convert.ToInt32(sqlDataReader["UserId"]),
                            };
                        }
                    }
                }
            }
            return user; 
        }
    }
}