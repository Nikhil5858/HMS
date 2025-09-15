using HMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class AdminDashboardActions
    {
        private readonly string connection;
        public AdminDashboardActions(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("ConnectionString");
        }
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

        public AdminDashBoard GetDashboardCounts()
        {
            AdminDashBoard dashboard = new AdminDashBoard();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_AdminDashboard_Counts", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dashboard.TotalAppointments = Convert.ToInt32(dr["TotalAppointments"]);
                    dashboard.CompletedAppointments = Convert.ToInt32(dr["CompletedAppointments"]);
                    dashboard.PendingAppointments = Convert.ToInt32(dr["PendingAppointments"]);
                    dashboard.TotalPatients = Convert.ToInt32(dr["TotalPatients"]);
                }
            }
            return dashboard;
        }

    }
}
