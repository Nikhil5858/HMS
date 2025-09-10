using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [SessionCheck]
    public class DoctorController : Controller
    {
        private readonly DoctorActions actions;
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
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Admin");
                }

                doctor.UserID = userId.Value;

                actions.InsertDoctor(doctor);
                TempData["DoctorMessage"] = "Doctor added successfully!";
            }
            catch (Exception ex)
            {
                TempData["DoctorMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DoctorEdit(int id)
        {
            try
            {
                var doctor = actions.GetDoctors().FirstOrDefault(x => x.DoctorID == id);
                if (doctor == null)
                {
                    TempData["DoctorMessage"] = "Doctor not found.";
                    return RedirectToAction("Index");
                }
                return View(doctor);
            }
            catch (Exception ex)
            {
                TempData["DoctorMessage"] = "Something Went Wrong";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DoctorEdit(Doctor doctor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(doctor);
                }

                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Admin");
                }

                doctor.UserID = userId.Value;

                actions.DoctorUpdate(doctor);
                TempData["DoctorMessage"] = "Doctor updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["DoctorMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("Index");
        }

        public IActionResult DoctorDelete(int id)
        {
            try
            {
                actions.DoctorDelete(id);
                TempData["DoctorMessage"] = "Doctor deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["DoctorMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("Index");
        }
    }
}