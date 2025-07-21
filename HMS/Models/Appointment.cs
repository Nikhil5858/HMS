namespace HMS.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentStatus { get; set; }
        public int Description { get; set; }
        public int SpecialRemarks { get; set; }
        public int TotalConsultedAmount { get; set; }

    }
}
