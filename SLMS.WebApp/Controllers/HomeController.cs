using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;
using System.Diagnostics;

namespace SLMS.WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DashboardService _dashboardService;

    public HomeController(
        ILogger<HomeController> logger,
        DashboardService dashboardService)
    {
        _logger = logger;
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _dashboardService.GetDashboardAsync();

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                            ?? HttpContext.TraceIdentifier
            });
    }
}