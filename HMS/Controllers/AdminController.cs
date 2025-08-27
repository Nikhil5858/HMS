using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminDashboardActions actions;
        private readonly DatabaseMethod databaseMethod;

        public AdminController(AdminDashboardActions actions, DatabaseMethod databaseMethod)
        {
            this.actions = actions;
            this.databaseMethod = databaseMethod;
        }
        public IActionResult Index()
        {
            var appointment = actions.TodaysAppointment();
            return View(appointment);
        }
        public IActionResult AdminDashboard()
        {
            var appointment = actions.TodaysAppointment();
            return View(appointment);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                SqlDataReader result = databaseMethod.check_Login(user.Email, user.Password);

                if (result.HasRows)
                {
                    var appointment = actions.TodaysAppointment();
                    return View("AdminDashboard", appointment);
                }
                else
                {
                    TempData["LoginMessage"] = "Invalid Email or Password!";
                    return View();
                }
            }
            return View();
        }

    }
}
