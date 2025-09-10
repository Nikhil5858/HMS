using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [SessionCheck] 
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
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Admin");
                }

                department.UserID = userId.Value;

                actions.InsertDepartment(department);
                TempData["DepartmentMessage"] = "Department added successfully!";
            }
            catch (Exception ex)
            {
                TempData["DepartmentMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                actions.DeleteDepartment(id);
                TempData["DepartmentMessage"] = "Department deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["DepartmentMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DepartmentEdit(int id)
        {
            try
            {
                var department = actions.GetDepartment().FirstOrDefault(d => d.DepartmentID == id);
                if (department == null)
                {
                    TempData["DepartmentMessage"] = "Department not found.";
                    return RedirectToAction("Index");
                }
                return View(department);
            }
            catch (Exception ex)
            {
                TempData["DepartmentMessage"] = "Something Went Wrong";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DepartmentEdit(Department department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(department);
                }

                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Admin");
                }

                department.UserID = userId.Value;

                actions.updateDepartment(department);
                TempData["DepartmentMessage"] = "Department updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["DepartmentMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("Index");
        }
    }
}