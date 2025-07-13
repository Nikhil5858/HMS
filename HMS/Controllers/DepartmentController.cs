using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            DepartmentActions departmentActions = new DepartmentActions();
            List<Department> departmentlist = departmentActions.GetDepartment();
            return View(departmentlist);
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
            DepartmentActions actions = new DepartmentActions();
            actions.InsertDepartment(department);

            TempData["Message"] = "Department added successfully!";
            return RedirectToAction("DepartmentAdd");
        }
        public IActionResult DeleteDepartment(int id)
        {
            DepartmentActions actions = new DepartmentActions();
            actions.DeleteDepartment(id);
            TempData["Message"] = "Department Deleted successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult DepartmentEdit()
        {
            return View();
        }
    }
}
