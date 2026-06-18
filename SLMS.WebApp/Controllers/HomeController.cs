using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
//using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebApp.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // token verification

            var token = HttpContext.Session.GetString("accesstoken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Token = token;
            return View();
        }

        public IActionResult Privacy()
        {
            var token = HttpContext.Session.GetString("accesstoken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
