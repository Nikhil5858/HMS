using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data;
 

namespace HMS.Models
{
    public class DatabaseMethod
    {
        string connection = "Data Source=DESKTOP-A45M567\\SQLEXPRESS01;Initial Catalog=HMS;Integrated Security=True;Encrypt=False";
        
        SqlConnection? con = null;

        public DatabaseMethod()
        {
            con = new SqlConnection(connection);
        }

        public SqlDataReader check_Login(string email, string password)
        {
            SqlCommand sqlCommand = new SqlCommand("SP_User_Login", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Email", email);
            sqlCommand.Parameters.AddWithValue("@Password", password);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            return sqlDataReader;
        }
    }
}
