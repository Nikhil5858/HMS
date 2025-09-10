using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [SessionCheck]
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
            try
            {
                var appointment = actions.TodaysAppointment();
                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something Went Wrong";
                return View(new List<Appointment>());
            }
        }
        [Route("/Admin/AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            try
            {
                var appointment = actions.TodaysAppointment();
                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something Went Wrong";
                return View(new List<Appointment>());
            }
        }

        [AllowAnonymous]
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            try
            {

                User? loggedInUser = databaseMethod.check_Login(user.Email, user.Password);

                if (loggedInUser != null)
                {
                    HttpContext.Session.SetInt32("UserId", loggedInUser.UserId);
                    return RedirectToAction("AdminDashboard","Admin");
                }
                else
                {
                    ViewData["LoginMessage"] = "Invalid Email or Password!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["LoginMessage"] = "Something Went Wrong";
                return View(user);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Admin");
        }

    }
}
