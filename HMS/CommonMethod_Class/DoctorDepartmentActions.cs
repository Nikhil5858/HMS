using HMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class DoctorDepartmentActions
    {
        private string connection = "Data Source=DESKTOP-A45M567\\SQLEXPRESS01;Initial Catalog=HMS;Integrated Security=True;Encrypt=False";

        public List<DoctorDepartment> GetAllDepartmentAndDocotr()
        {
            List<DoctorDepartment> list = new List<DoctorDepartment>();

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_DoctorDepartment_SelectByUserID", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new DoctorDepartment
                    {
                        DoctorDepartmentID = Convert.ToInt32(reader["DoctorDepartmentID"]),
                        DoctorID = Convert.ToInt32(reader["DoctorID"]),
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DoctorName = reader["DoctorName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString()
                    });
                }
            }
            return list;
        }

        public void InsertDoctorDepartment(DoctorDepartment doctorDepartment)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_DoctorDepartment_Insert", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("DoctorID",doctorDepartment.DoctorID);
                sqlCommand.Parameters.AddWithValue("DepartmentID", doctorDepartment.DepartmentID);
                sqlCommand.Parameters.AddWithValue("UserID", doctorDepartment.UserID);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteDoctorDepartment(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_DoctortoDepartment_Delete", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("DoctorDepartmentID",id);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateDoctorDepartment(DoctorDepartment doctorDepartment)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Doctor_Department_Update", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@DoctorDepartmentID", doctorDepartment.DoctorDepartmentID);
                sqlCommand.Parameters.AddWithValue("@DoctorID", doctorDepartment.DoctorID);
                sqlCommand.Parameters.AddWithValue("@DepartmentID", doctorDepartment.DepartmentID);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public DoctorDepartment GetDoctorDepartmentById(int id)
        {
            DoctorDepartment doctorDepartment = new DoctorDepartment();

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_DoctorDepartment_SelectByID", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@DoctorDepartmentID", id);
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    doctorDepartment.DoctorDepartmentID = Convert.ToInt32(reader["DoctorDepartmentID"]);
                    doctorDepartment.DoctorID = Convert.ToInt32(reader["DoctorID"]);
                    doctorDepartment.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                }
            }
            return doctorDepartment;
        }

        public (List<Doctor> doctors,List<Department> departments) GetDoctorAndDepartment()
        {
            List<Doctor> doctors_list = new List<Doctor>();
            List<Department> departments_list = new List<Department>();

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_DoctorDepartment_GetDropdownData",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        doctors_list.Add(new Doctor
                        {
                            DoctorID = Convert.ToInt32(sqlDataReader["DoctorID"]),
                            Name = sqlDataReader["DoctorName"].ToString()
                        });
                    }

                    if (sqlDataReader.NextResult())
                    {
                        while (sqlDataReader.Read())
                        {
                            departments_list.Add(new Department
                            {
                                DepartmentID = Convert.ToInt32(sqlDataReader["DepartmentID"]),
                                DepartmentName = sqlDataReader["DepartmentName"].ToString()
                            });
                        }
                    }
                }
            }
            return (doctors_list,departments_list);
        }
        public void DeleteDepartmentsByDoctorId(int doctorId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                string query = "DELETE FROM DoctorDepartment WHERE DoctorID = @DoctorID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@DoctorID", doctorId);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
