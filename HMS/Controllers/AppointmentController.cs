using HMS.CommonMethod_Class;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
        public IActionResult AppointmentEdit()
        {
            return View();
        }
    }
}
