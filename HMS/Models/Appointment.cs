using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Select Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage = "Select Appointment Status")]
        public string AppointmentStatus { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter SpecialRemarks")]
        public string SpecialRemarks { get; set; }
        [Required(ErrorMessage = "Enter Total Amount")]
        public int TotalConsultedAmount { get; set; }

    }
}
