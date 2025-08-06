using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentActions actions = new DepartmentActions();

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
            department.UserID = 1;
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

            department.UserID = 1;
            actions.updateDepartment(department);
            TempData["DepartmentMessage"] = "Department updated successfully!";
            return RedirectToAction("Index");
        }
    }
}
