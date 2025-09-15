using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Controllers
{
    [SessionCheck]
    public class AppointmentController : Controller
    {
        private readonly AppointmentActions actions;
        public AppointmentController(AppointmentActions actions)
        {
            this.actions = actions;
        }

        public IActionResult Index(string doctorName, string patientName, string status, DateTime? date)
        {
            List<Appointment> list;

            if (string.IsNullOrEmpty(doctorName) && string.IsNullOrEmpty(patientName) && string.IsNullOrEmpty(status) && !date.HasValue)
            {
                list = actions.GetAppointment();
            }
            else
            {
                list = actions.GetFilteredAppointments(doctorName, patientName, status, date);
            }

            ViewData["doctorName"] = doctorName;
            ViewData["patientName"] = patientName;
            ViewData["status"] = status;
            ViewData["date"] = date?.ToString("yyyy-MM-dd");

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
            try
            {
                if (!ModelState.IsValid)
                {
                    int? userId = HttpContext.Session.GetInt32("UserId");
                    if (userId == null)
                    {
                        return RedirectToAction("Login", "Admin");
                    }
                    appointment.UserID = userId.Value;
                    actions.InsertAppointment(appointment);
                    TempData["AppointmentMessage"] = "Appointment added successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["AppointmentMessage"] = "Something Went Wrong";
                return RedirectToAction("Index");
            }

            var (doctors, patients) = actions.GetDoctorsAndPatients();
            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name", appointment.DoctorID);
            ViewBag.PatientList = new SelectList(patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        public IActionResult AppointmentEdit(int id)
        {
            try
            {
                var appointment = actions.GetAppointmentById(id);
                if (appointment == null)
                {
                    TempData["AppointmentMessage"] = "Appointment not found.";
                    return RedirectToAction("Index");
                }

                var (doctors, patients) = actions.GetDoctorsAndPatients();
                ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name", appointment.DoctorID);
                ViewBag.PatientList = new SelectList(patients, "PatientID", "Name", appointment.PatientID);
                return View(appointment);
            }
            catch (Exception ex)
            {
                TempData["AppointmentMessage"] = "Something Went Wrong";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult AppointmentEdit(Appointment appointment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    int? userId = HttpContext.Session.GetInt32("UserId");
                    if (userId == null)
                    {
                        return RedirectToAction("Login", "Admin");
                    }
                    appointment.UserID = userId.Value; 
                    actions.UpdateAppointment(appointment);
                    TempData["AppointmentMessage"] = "Appointment updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["AppointmentMessage"] = "Something Went Wrong";
                return RedirectToAction("Index");
            }

            var (doctors, patients) = actions.GetDoctorsAndPatients();
            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name", appointment.DoctorID);
            ViewBag.PatientList = new SelectList(patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        public IActionResult DeleteAppointment(int id)
        {
            try
            {
                actions.DeleteAppointment(id);
                TempData["AppointmentMessage"] = "Appointment Deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["AppointmentMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("Index");
        }
    }
}