using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        private DoctorActions actions = new DoctorActions();

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
            doctor.UserID = 1;
            actions.InsertDoctor(doctor);
            TempData["Message"] = "Doctor added successfully!";
            return RedirectToAction("DoctorAdd");
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

            doctor.UserID = 1;
            actions.DoctorUpdate(doctor);
            TempData["Message"] = "Doctor updated successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult DoctorDelete(int id)
        {
            actions.DoctorDelete(id);
            TempData["Message"] = "Doctor deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
