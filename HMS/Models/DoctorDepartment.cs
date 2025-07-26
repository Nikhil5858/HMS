using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class DoctorDepartment
    {

        public int DoctorDepartmentID { get; set; }
        [Required(ErrorMessage ="Select Doctor Name")]
        public int DoctorID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage ="Select Department Name")]
        public List<int> SelectedDepartmentID { get; set; }
        public int DepartmentID { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }

    }
}
