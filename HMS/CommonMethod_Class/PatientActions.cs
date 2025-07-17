using HMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class PatientActions
    {
        private string connection = "Data Source=DESKTOP-A45M567\\SQLEXPRESS01;Initial Catalog=HMS;Integrated Security=True;Encrypt=False";

        public void InsertPatient(Patient patient)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Patient_Insert",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserID", patient.UserID);
                sqlCommand.Parameters.AddWithValue("@Name",patient.Name);
                sqlCommand.Parameters.AddWithValue("@DateOfBirth",patient.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("@Gender",patient.Gender);
                sqlCommand.Parameters.AddWithValue("@Email",patient.Email);
                sqlCommand.Parameters.AddWithValue("@Phone", patient.Phone);
                sqlCommand.Parameters.AddWithValue("@Address", patient.Address);
                sqlCommand.Parameters.AddWithValue("@City",patient.City);
                sqlCommand.Parameters.AddWithValue("@State",patient.State);
                sqlCommand.Parameters.AddWithValue("@IsActive", patient.IsActive);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public List<Patient> GetPatients() 
        {
            List<Patient> list = new List<Patient>();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Patients_Select", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Patient patient = new Patient
                    {
                        PatientID = Convert.ToInt32(reader["PatientID"]),
                        Name = reader["Name"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Email = reader["Email"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Phone = reader["Phone"].ToString(),
                        City = reader["City"].ToString(),
                        Address = reader["Address"].ToString(),
                        State = reader["State"].ToString()
                    };
                    list.Add(patient);
                }
            }
            return list;
        }

        public void Patientupdate(Patient patient)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Patient_Update",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserID",patient.UserID);
                sqlCommand.Parameters.AddWithValue("@PatientID", patient.PatientID);
                sqlCommand.Parameters.AddWithValue("@Name", patient.Name);
                sqlCommand.Parameters.AddWithValue("@Gender", patient.Gender);
                sqlCommand.Parameters.AddWithValue("@Email", patient.Email);
                sqlCommand.Parameters.AddWithValue("@Phone", patient.Phone);
                sqlCommand.Parameters.AddWithValue("@City", patient.City);
                sqlCommand.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("@Address", patient.Address);
                sqlCommand.Parameters.AddWithValue("@State", patient.State);
                sqlCommand.Parameters.AddWithValue("@IsActive", patient.IsActive);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void PatientDelete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand sqlCommand = new SqlCommand("sp_Patient_Delete",sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@PatientID", id);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
