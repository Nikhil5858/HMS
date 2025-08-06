namespace HMS.Models
{
    public class AdminDashBoard
    {
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public int TotalAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int PendingAppointments { get; set; }
        public int TotalPatients { get; set; }

    }
}
