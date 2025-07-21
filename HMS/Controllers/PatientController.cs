using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        private PatientActions actions = new PatientActions();

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
            patient.UserID = 1;
            actions.InsertPatient(patient);

            TempData["Message"] = "Patient Added Successfully!";
            return RedirectToAction("PatientAdd");
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

            patient.UserID = 1;
            actions.Patientupdate(patient);

            TempData["Message"] = "Patient Updated Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult PatientDelete(int id)
        {
            actions.PatientDelete(id);

            TempData["Message"] = "Patient Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
