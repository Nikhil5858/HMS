using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        private DoctorActions actions;
        public DoctorController(DoctorActions actions)
        {
            this.actions = actions;
        }

        public IActionResult Index()
        {
            var doctors = actions.GetDoctors();
            return View(doctors);
        }

        [HttpGet]
        public IActionResult DoctorAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DoctorAdd(Doctor doctor)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            doctor.UserID = userId.Value;

            actions.InsertDoctor(doctor);
            TempData["DoctorMessage"] = "Doctor added successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DoctorEdit(int id)
        {
            var doctor = actions.GetDoctors().FirstOrDefault(x => x.DoctorID == id);
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }
            return View(doctor);
        }

        [HttpPost]
        public IActionResult DoctorEdit(Doctor doctor)
        {
            if (!ModelState.IsValid) return View(doctor);

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            doctor.UserID = userId.Value;

            actions.DoctorUpdate(doctor);
            TempData["DoctorMessage"] = "Doctor updated successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult DoctorDelete(int id)
        {
            actions.DoctorDelete(id);
            TempData["DoctorMessage"] = "Doctor deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
