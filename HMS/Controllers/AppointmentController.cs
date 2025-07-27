using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Controllers
{
    public class AppointmentController : Controller
    {
        private AppointmentActions actions = new AppointmentActions();
        public IActionResult Index()
        {
            List<Appointment> list = actions.GetAppointment();
            return View(list);
        }
        public IActionResult AppointmentAdd()
        {
            var (doctors, patients) = actions.GetDoctorsAndPatients();

            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name");
            ViewBag.PatientList = new SelectList(patients, "PatientID", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult AppointmentAdd(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                appointment.UserID = 1;
                actions.InsertAppointment(appointment);

                TempData["Message"] = "Appointment added successfully!";
                return RedirectToAction("AppointmentAdd");
            }
            return View(appointment);
        }

        public IActionResult AppointmentEdit(int id)
        {
            var appointment = actions.GetAppointmentById(id);
            var (doctors, patients) = actions.GetDoctorsAndPatients();

            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name", appointment.DoctorID);
            ViewBag.PatientList = new SelectList(patients, "PatientID", "Name", appointment.PatientID);

            return View(appointment);
        }

        [HttpPost]
        public IActionResult AppointmentEdit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                actions.UpdateAppointment(appointment);
                TempData["Message"] = "Appointment updated successfully!";
                return RedirectToAction("Index");
            }

            var (doctors, patients) = actions.GetDoctorsAndPatients();
            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name", appointment.DoctorID);
            ViewBag.PatientList = new SelectList(patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }
        public IActionResult DeleteAppointment(int id)
        {
            actions.DeleteAppointment(id);
            TempData["Message"] = "Appointment Deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
