using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DoctorAdd()
        {
            return View();
        }
        public IActionResult DoctorEdit()
        {
            return View();
        }
    }
}
