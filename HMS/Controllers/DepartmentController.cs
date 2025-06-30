using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
