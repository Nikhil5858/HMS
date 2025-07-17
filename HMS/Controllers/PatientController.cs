using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            PatientActions patientActions = new PatientActions();
            List<Patient> patientlist = patientActions.GetPatients();
            return View(patientlist);
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
            PatientActions patientActions = new PatientActions();
            patientActions.InsertPatient(patient);

            TempData["Message"] = "Patient Added Succesfully";
            return RedirectToAction("PatientAdd");
        }
        [HttpGet]
        public IActionResult PatientEdit(int id)
        {
            PatientActions patientActions = new PatientActions();
            List<Patient> patientlist = patientActions.GetPatients();
            Patient patient = patientlist.FirstOrDefault(p=>p.PatientID==id);

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
            patient.UserID=1;
            PatientActions patientActions = new PatientActions();
            patientActions.Patientupdate(patient);
            TempData["Message"] = "Patient Updated Successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult PatientDelete(int id)
        {
            PatientActions patientAction = new PatientActions();
            patientAction.PatientDelete(id);

            TempData["Message"] = "Patient Delete Succesfully";
            return RedirectToAction("Index");
        }
    }
}
