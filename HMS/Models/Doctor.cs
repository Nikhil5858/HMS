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
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Qualification")]
        public string Qualification { get; set; }
        [Required(ErrorMessage = "Enter Specialization")]
        public string Specialization { get; set; }
        public Boolean IsActive { get; set; }

    }
}
