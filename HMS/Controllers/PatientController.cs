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
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Admin");
                }

                patient.UserID = userId.Value;
                actions.InsertPatient(patient);

                TempData["PatientMessage"] = "Patient Added Successfully!";
            }
            catch (Exception ex)
            {
                TempData["PatientMessage"] = "Something Went Wrong!";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PatientEdit(int id)
        {
            try
            {
                Patient? patient = actions.GetPatients().FirstOrDefault(p => p.PatientID == id);

                if (patient == null)
                {
                    TempData["PatientMessage"] = "Patient not found.";
                    return RedirectToAction("Index");
                }
                return View(patient);
            }
            catch (Exception ex)
            {
                TempData["PatientMessage"] = "Something Went Wrong!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult PatientEdit(Patient patient)
        {
            try
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
            }
            catch (Exception ex)
            {
                TempData["PatientMessage"] = "Something Went Wrong!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult PatientDelete(int id)
        {
            try
            {
                actions.PatientDelete(id);
                TempData["PatientMessage"] = "Patient Deleted Successfully!";
            }
            catch (Exception ex)
            {
                TempData["PatientMessage"] = "Something Went Wrong!";
            }
            return RedirectToAction("Index");
        }
    }
}