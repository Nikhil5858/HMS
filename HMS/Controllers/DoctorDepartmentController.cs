using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Controllers
{
    public class DoctorDepartmentController : Controller
    {
        private readonly DoctorDepartmentActions actions;
        public DoctorDepartmentController(DoctorDepartmentActions actions)
        {
            this.actions = actions;
        }

        public IActionResult Index()
        {
            List<DoctorDepartment> doctorDepartmentList = actions.GetAllDepartmentAndDocotr();
            return View(doctorDepartmentList);
        }

        public IActionResult DoctorDepartmentAdd()
        {
            var (doctors, departments) = actions.GetDoctorAndDepartment();

            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name");
            ViewBag.DepartmentList = departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentID.ToString(),
                Text = d.DepartmentName
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult DoctorDepartmentAdd(DoctorDepartment doctorDepartment)
        {
            doctorDepartment.UserID = 1;

            foreach (var deptId in doctorDepartment.SelectedDepartmentID)
            {
                var data = new DoctorDepartment
                {
                    DoctorID = doctorDepartment.DoctorID,
                    DepartmentID = deptId,
                    UserID = doctorDepartment.UserID
                };

                actions.InsertDoctorDepartment(data);
            }

            TempData["DoctorDepartment"] = "Doctor Department Added Successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DoctorDepartmentEdit(int id)
        {
            DoctorDepartment doctorDepartment = actions.GetDoctorDepartmentById(id);
            var (doctors, departments) = actions.GetDoctorAndDepartment();
            ViewBag.DoctorList = new SelectList(doctors, "DoctorID", "Name");
            var selectedDepartments = actions.GetAllDepartmentAndDocotr()
            .Where(dd => dd.DoctorID == doctorDepartment.DoctorID)
            .Select(dd => dd.DepartmentID)
            .ToList();
            doctorDepartment.SelectedDepartmentID = selectedDepartments;
            ViewBag.DepartmentList = departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentID.ToString(),
                Text = d.DepartmentName
            }).ToList();
            return View(doctorDepartment);
        }
        [HttpPost]
        public IActionResult DoctorDepartmentEdit(DoctorDepartment doctorDepartment)
        {
            doctorDepartment.UserID = 1;
            actions.DeleteDepartmentsByDoctorId(doctorDepartment.DoctorID);
            foreach (var deptId in doctorDepartment.SelectedDepartmentID)
            {
                var data = new DoctorDepartment
                {
                    DoctorID = doctorDepartment.DoctorID,
                    DepartmentID = deptId,
                    UserID = doctorDepartment.UserID
                };

                actions.InsertDoctorDepartment(data);
            }

            TempData["DoctorDepartment"] = "Doctor Department Updated Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult DoctorDepartmentDelete(int id)
        {
            actions.DeleteDoctorDepartment(id); 

            TempData["DoctorDepartment"] = "Doctor Department Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
