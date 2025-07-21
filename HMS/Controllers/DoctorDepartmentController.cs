using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Controllers
{
    public class DoctorDepartmentController : Controller
    {
        private DoctorDepartmentActions actions = new DoctorDepartmentActions();

        public IActionResult Index()
        {
            List<DoctorDepartment> doctorDepartmentList = actions.GetAllDepartmentAndDocotr();
            return View(doctorDepartmentList);
        }

        public IActionResult DoctorDepartmentAdd()
        {
            var (doctors, departments) = actions.GetDoctorAndDepartment();

            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name");
            ViewBag.DepartmentList = new SelectList(departments, "DepartmentID", "DepartmentName");

            return View();
        }

        [HttpPost]
        public IActionResult DoctorDepartmentAdd(DoctorDepartment doctorDepartment)
        {
            doctorDepartment.UserID = 1;
            actions.InsertDoctorDepartment(doctorDepartment);

            TempData["Message"] = "Doctor Department Added Successfully!";
            return RedirectToAction("DoctorDepartmentAdd");
        }

        [HttpGet]
        public IActionResult DoctorDepartmentEdit(int id)
        {
            DoctorDepartment doctorDepartment = actions.GetDoctorDepartmentById(id);
            var (doctors, departments) = actions.GetDoctorAndDepartment();

            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name");
            ViewBag.DepartmentList = new SelectList(departments, "DepartmentID", "DepartmentName");

            return View(doctorDepartment);
        }

        [HttpPost]
        public IActionResult DoctorDepartmentEdit(DoctorDepartment doctorDepartment)
        {
            doctorDepartment.UserID = 1;
            actions.UpdateDoctorDepartment(doctorDepartment);

            TempData["Message"] = "Doctor Department Updated Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult DoctorDepartmentDelete(int id)
        {
            actions.DeleteDoctorDepartment(id);

            TempData["Message"] = "Doctor Department Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
