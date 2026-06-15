using Microsoft.AspNetCore.Mvc;

namespace SLMS.WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Employee()
        {
            return View();
        }


    }
}
