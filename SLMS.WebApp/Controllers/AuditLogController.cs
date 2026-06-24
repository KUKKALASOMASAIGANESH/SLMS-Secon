using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AuditLogController : Controller
{
    private readonly AuditLogService _service;

    public AuditLogController(
        AuditLogService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var logs =
                await _service.GetAllAsync();

            return View(logs);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load audit logs.";

            return View(
                new List<AuditLogViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var log =
                await _service.GetByIdAsync(id);

            if (log == null)
                return NotFound();

            return View(log);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load audit log details.";

            return RedirectToAction(nameof(Index));
        }
    }
}