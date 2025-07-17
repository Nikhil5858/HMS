using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            DoctorActions actions = new DoctorActions();
            List<Doctor> doctorlist = actions.GetDoctors();
            return View(doctorlist);
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
            DoctorActions doctorActions = new DoctorActions();
            doctorActions.InsertDoctor(doctor);

            TempData["Message"] = "Doctor Added Successfully";
            return RedirectToAction("DoctorAdd");
        }
        [HttpGet]
        public IActionResult DoctorEdit(int id)
        {
            DoctorActions doctorActions = new DoctorActions();
            List<Doctor> doctorlist = doctorActions.GetDoctors();
            Doctor doctor = doctorlist.FirstOrDefault(x=>x.DoctorID==id);

            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        [HttpPost]
        public IActionResult DoctorEdit(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }
            doctor.UserID = 1;
            DoctorActions doctorActions = new DoctorActions();
            doctorActions.DoctorUpdate(doctor);
            TempData["Message"] = "Doctor Updated Successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult DoctorDelete(int id)
        {
            DoctorActions actions = new DoctorActions();
            actions.DoctorDelete(id);

            TempData["Message"] = "Doctor Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
