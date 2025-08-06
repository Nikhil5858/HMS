using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HMS.Controllers
{
    public class AdminController : Controller
    {
        private AdminDashboardActions actions = new AdminDashboardActions();
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
        public IActionResult Login(string email,string password)
        {
            DatabaseMethod databasemethod = new DatabaseMethod();
            SqlDataReader result = databasemethod.check_Login(email, password);
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

    }
}
