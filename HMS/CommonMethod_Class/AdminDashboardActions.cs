using HMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class AdminDashboardActions
    {
        private string connection = "Data Source=DESKTOP-A45M567\\SQLEXPRESS01;Initial Catalog=HMS;Integrated Security=True;Encrypt=False";

        public List<AdminDashBoard> TodaysAppointment()
        {
            List<AdminDashBoard> list = new List<AdminDashBoard>();
            using(SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Appointment_TodaysAppointment",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    AdminDashBoard adminDashBoard = new AdminDashBoard 
                    {
                        DoctorName = sqlDataReader["DoctorName"].ToString(),
                        PatientName = sqlDataReader["PatientName"].ToString(),
                        AppointmentDate = Convert.ToDateTime(sqlDataReader["AppointmentDate"]),
                        AppointmentStatus = sqlDataReader["AppointmentStatus"].ToString()
                    };
                    list.Add(adminDashBoard);
                }
            }
            return list;
        }
    }
}
