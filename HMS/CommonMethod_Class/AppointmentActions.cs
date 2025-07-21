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
                sqlCommand.Parameters.AddWithValue("UserID", appointment.UserID);
                sqlCommand.Parameters.AddWithValue("AppointmentStatus", appointment.AppointmentStatus);
                sqlCommand.Parameters.AddWithValue("AppointmentDate", appointment.AppointmentDate);
                sqlCommand.Parameters.AddWithValue("Description", appointment.Description);
                sqlCommand.Parameters.AddWithValue("TotalConsultedAmount", appointment.TotalConsultedAmount);
                sqlCommand.Parameters.AddWithValue("SpecialRemarks", appointment.SpecialRemarks);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
