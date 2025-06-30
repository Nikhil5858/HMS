using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AppointmentAdd()
        {
            return View();
        }
        public IActionResult AppointmentEdit()
        {
            return View();
        }
    }
}
