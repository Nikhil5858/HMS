using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [SessionCheck]
    public class PatientController : Controller
    {
        private readonly PatientActions actions;
        public PatientController(PatientActions actions)
        {
            this.actions = actions;
        }

        public IActionResult Index()
        {
            List<Patient> patientList = actions.GetPatients();
            return View(patientList);
        }

        [HttpGet]
        public IActionResult PatientAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PatientAdd(Patient patient)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            patient.UserID = userId.Value;

            actions.InsertPatient(patient);

            TempData["PatientMessage"] = "Patient Added Successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PatientEdit(int id)
        {
            List<Patient> patientList = actions.GetPatients();
            Patient patient = patientList.FirstOrDefault(p => p.PatientID == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public IActionResult PatientEdit(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            patient.UserID = userId.Value;

            actions.Patientupdate(patient);

            TempData["PatientMessage"] = "Patient Updated Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult PatientDelete(int id)
        {
            actions.PatientDelete(id);

            TempData["PatientMessage"] = "Patient Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
