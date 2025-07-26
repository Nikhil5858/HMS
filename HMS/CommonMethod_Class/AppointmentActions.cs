using HMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class AppointmentActions
    {
        private string connection = "Data Source=DESKTOP-A45M567\\SQLEXPRESS01;Initial Catalog=HMS;Integrated Security=True;Encrypt=False";

        public void InsertAppointment(Appointment appointment)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Appointment_Insert",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("DoctorID", appointment.DoctorID);
                sqlCommand.Parameters.AddWithValue("PatientID", appointment.PatientID);
                sqlCommand.Parameters.AddWithValue("UserID", appointment.UserID == 0 ? 1 : appointment.UserID);
                sqlCommand.Parameters.AddWithValue("AppointmentStatus", appointment.AppointmentStatus);
                sqlCommand.Parameters.AddWithValue("Email", appointment.Email);
                sqlCommand.Parameters.AddWithValue("Specialization", appointment.Specialization);
                sqlCommand.Parameters.AddWithValue("AppointmentDate", appointment.AppointmentDate);
                sqlCommand.Parameters.AddWithValue("Description", appointment.Description);
                sqlCommand.Parameters.AddWithValue("TotalConsultedAmount", appointment.TotalConsultedAmount);
                sqlCommand.Parameters.AddWithValue("SpecialRemarks", appointment.SpecialRemarks);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public List<Appointment> GetAppointment()
        {
            List<Appointment> list = new List<Appointment>();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Appointment_select", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        AppointmentID = Convert.ToInt32(sqlDataReader["AppointmentID"]),
                        AppointmentDate = Convert.ToDateTime(sqlDataReader["AppointmentDate"]),
                        AppointmentStatus = sqlDataReader["AppointmentStatus"].ToString(),
                        Description = sqlDataReader["Description"].ToString(),
                        SpecialRemarks = sqlDataReader["SpecialRemarks"].ToString(),
                        TotalConsultedAmount = Convert.ToInt32(sqlDataReader["TotalConsultedAmount"]),
                        DoctorName = sqlDataReader["DoctorName"].ToString(),
                        PatientName = sqlDataReader["PatientName"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString(),
                        Email = sqlDataReader["PatientEmail"].ToString(),
                        Specialization = sqlDataReader["DoctorSpecialization"].ToString()
                    };
                    list.Add(appointment);
                }
            }
            return list;
        }

    }
}
