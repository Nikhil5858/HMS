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
            string query = "select Email,Password from [User] where Email=@email And Password=@password";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.AddWithValue("@email", email);
            sqlCommand.Parameters.AddWithValue("@password", password);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            return sqlDataReader;
        }



    }
}
