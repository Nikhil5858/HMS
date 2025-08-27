using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Doctor name is required")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Select Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Select Appointment Status")]
        public string AppointmentStatus { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Special Remarks")]
        public string SpecialRemarks { get; set; }

        [Required(ErrorMessage = "Enter Total Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public int TotalConsultedAmount { get; set; }
    }
}
