using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorDepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DoctorDepartmentAdd()
        {
            return View();
        }
        public IActionResult DoctorDepartmentEdit()
        {
            return View();
        }
    }
}
