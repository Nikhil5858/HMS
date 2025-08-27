using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HMS.CommonMethod_Class
{
    public class AppointmentActions
    {
        private readonly string connection;
        public AppointmentActions(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("ConnectionString");
        }
        public void InsertAppointment(Appointment appointment)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Appointment_Insert", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("DoctorID", appointment.DoctorID);
                sqlCommand.Parameters.AddWithValue("PatientID", appointment.PatientID);
                sqlCommand.Parameters.AddWithValue("AppointmentDate", appointment.AppointmentDate);
                sqlCommand.Parameters.AddWithValue("AppointmentStatus", appointment.AppointmentStatus);
                sqlCommand.Parameters.AddWithValue("Description", appointment.Description);
                sqlCommand.Parameters.AddWithValue("SpecialRemarks", appointment.SpecialRemarks);
                sqlCommand.Parameters.AddWithValue("UserID", appointment.UserID == 0 ? 1 : appointment.UserID);
                sqlCommand.Parameters.AddWithValue("TotalConsultedAmount", appointment.TotalConsultedAmount);
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
        public (List<Doctor> doctors, List<Patient> patients) GetDoctorsAndPatients()
        {
            List<Doctor> doctors = new List<Doctor>();
            List<Patient> patients = new List<Patient>();

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                var doctorCommand = new SqlCommand("SELECT DoctorID, Name FROM Doctor", sqlConnection);
                using (SqlDataReader reader = doctorCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        doctors.Add(new Doctor
                        {
                            DoctorID = Convert.ToInt32(reader["DoctorID"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }

                var patientCommand = new SqlCommand("SELECT PatientID, Name FROM Patient", sqlConnection);
                using (SqlDataReader reader = patientCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientID = Convert.ToInt32(reader["PatientID"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }

            return (doctors, patients);
        }
        public Appointment GetAppointmentById(int id)
        {
            Appointment appointment = null;
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_Appointment_selectById", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppointmentID", id);
                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        appointment = new Appointment
                        {
                            AppointmentID = Convert.ToInt32(reader["AppointmentID"]),
                            DoctorID = Convert.ToInt32(reader["DoctorID"]),
                            PatientID = Convert.ToInt32(reader["PatientID"]),
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            AppointmentStatus = reader["AppointmentStatus"].ToString(),
                            Description = reader["Description"].ToString(),
                            SpecialRemarks = reader["SpecialRemarks"].ToString(),
                            TotalConsultedAmount = Convert.ToInt32(reader["TotalConsultedAmount"])
                        };
                    }
                }
            }
            return appointment;
        }

        public void UpdateAppointment(Appointment appointment)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_Appointment_Update", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AppointmentID", appointment.AppointmentID);
                cmd.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
                cmd.Parameters.AddWithValue("@PatientID", appointment.PatientID);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                cmd.Parameters.AddWithValue("@AppointmentStatus", appointment.AppointmentStatus);
                cmd.Parameters.AddWithValue("@Description", appointment.Description);
                cmd.Parameters.AddWithValue("@SpecialRemarks", appointment.SpecialRemarks);
                cmd.Parameters.AddWithValue("@TotalConsultedAmount", appointment.TotalConsultedAmount);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteAppointment(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Appointment_Delete",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("AppointmentId", id);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
