using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.Models
{
    public class DatabaseMethod
    {
        private readonly string connection;

        public DatabaseMethod(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("HMSConnection");
        }

        public SqlDataReader check_Login(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_User_Login", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Email", email);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                con.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return sqlDataReader;
            }
        }
    }
}
