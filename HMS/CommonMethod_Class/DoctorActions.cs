using HMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class DoctorActions
    {
        private string connection = "Data Source=DESKTOP-A45M567\\SQLEXPRESS01;Initial Catalog=HMS;Integrated Security=True;Encrypt=False";

        public void InsertDoctor(Doctor doctor)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Doctor_Insert",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name",doctor.Name);
                sqlCommand.Parameters.AddWithValue("@UserID",doctor.UserID);
                sqlCommand.Parameters.AddWithValue("@Phone", doctor.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", doctor.Email);
                sqlCommand.Parameters.AddWithValue("@Qualification", doctor.Qualification);
                sqlCommand.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> list = new List<Doctor>();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Doctor_Select",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor
                    {
                        Name = reader["Name"].ToString(),
                        DoctorID = Convert.ToInt32(reader["DoctorID"]),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"].ToString(),
                        Qualification = reader["Qualification"].ToString(),
                        Specialization = reader["Specialization"].ToString()
                    };
                    list.Add(doctor);
                }
            }
            return list;
        }

        public void DoctorDelete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand sqlCommand = new SqlCommand("sp_Doctor_Delete",sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@DoctorID", id);
            sqlConnection.Open();   
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void DoctorUpdate(Doctor doctor)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Doctor_Update",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", doctor.Name);
                sqlCommand.Parameters.AddWithValue("@UserID", doctor.UserID);
                sqlCommand.Parameters.AddWithValue("@DoctorID", doctor.DoctorID);
                sqlCommand.Parameters.AddWithValue("@IsActive", doctor.IsActive);
                sqlCommand.Parameters.AddWithValue("@Phone", doctor.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", doctor.Email);
                sqlCommand.Parameters.AddWithValue("@Qualification", doctor.Qualification);
                sqlCommand.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
