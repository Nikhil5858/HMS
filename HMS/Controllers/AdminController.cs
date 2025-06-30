using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HMS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
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
                return View("AdminDashboard");
            }
            else {
                TempData["Message"] = "Invalid Email or Password!";
                return View();
            }
        }
    }
}
