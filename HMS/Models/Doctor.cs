using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "Enter Doctor Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Phone No")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Qualification")]
        public string Qualification { get; set; }
        [Required(ErrorMessage = "Enter Specialization")]
        public string Specialization { get; set; }
        public Boolean IsActive { get; set; }

    }
}
