using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;

namespace SLMS.WebAPI.Controllers;


//[Authorize]
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(
        IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard()
    {
        var dashboard =
            await _dashboardService.GetDashboardAsync();

        return Ok(dashboard);
    }
    [HttpGet("analytics")]
    public async Task<IActionResult> GetAnalytics()
    {
        var result =
            await _dashboardService.GetAnalyticsAsync();

        return Ok(result);
    }

}