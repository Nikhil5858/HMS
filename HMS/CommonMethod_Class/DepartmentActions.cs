using HMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class DepartmentActions
    {
        private readonly string connection;
        public DepartmentActions(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("ConnectionString");
        }
        public void InsertDepartment(Department department)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Department_Insert",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@DepartmentName",department.DepartmentName);
                sqlCommand.Parameters.AddWithValue("@Description",department.Description);
                sqlCommand.Parameters.AddWithValue("@IsActive",1);
                sqlCommand.Parameters.AddWithValue("@UserID",department.UserID);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
        public List<Department> GetDepartment() 
        {
            List<Department> list = new List<Department>();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Department_Select",sqlConnection);
                sqlCommand.CommandType=CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read()) 
                {
                    Department department = new Department
                    {
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Description = reader["Description"].ToString(),

                    };
                    list.Add(department);
                }
            }
        return list;
        }
        public void DeleteDepartment(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Department_Delete",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@DepartmentID",id);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void updateDepartment(Department department)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Department_Update",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
                sqlCommand.Parameters.AddWithValue("@DepartmentName",department.DepartmentName);
                sqlCommand.Parameters.AddWithValue("@Description", department.Description);
                sqlCommand.Parameters.AddWithValue("@UserID", department.UserID);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
