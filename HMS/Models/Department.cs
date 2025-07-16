using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        public int UserID { get; set; }
        
        [Required(ErrorMessage = "Enter Department Name")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Enter Department Description")]
        public string Description { get; set; }

    }
}
