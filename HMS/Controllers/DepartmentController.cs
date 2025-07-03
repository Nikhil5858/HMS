using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DepartmentAdd()
        {
            return View();
        }
        public IActionResult DepartmentEdit()
        {
            return View();
        }
    }
}
