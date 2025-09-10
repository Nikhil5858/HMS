using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentActions actions;
        public DepartmentController(DepartmentActions actions)
        {
            this.actions = actions;
        }

        public IActionResult Index()
        {
            var departments = actions.GetDepartment();
            return View(departments);
        }

        [HttpGet]
        public IActionResult DepartmentAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DepartmentAdd(Department department)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            department.UserID = userId.Value;

            actions.InsertDepartment(department);
            TempData["DepartmentMessage"] = "Department added successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDepartment(int id)
        {
            actions.DeleteDepartment(id);
            TempData["DepartmentMessage"] = "Department deleted successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DepartmentEdit(int id)
        {
            var department = actions.GetDepartment().FirstOrDefault(d => d.DepartmentID == id);
            return department == null ? NotFound() : View(department);
        }

        [HttpPost]
        public IActionResult DepartmentEdit(Department department)
        {
            if (!ModelState.IsValid) return View(department);

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            department.UserID = userId.Value;

            actions.updateDepartment(department);
            TempData["DepartmentMessage"] = "Department updated successfully!";
            return RedirectToAction("Index");
        }
    }
}
