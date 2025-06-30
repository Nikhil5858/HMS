using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PatientAdd()
        {
            return View();
        }
        public IActionResult PatientEdit()
        {
            return View();
        }
    }
}
