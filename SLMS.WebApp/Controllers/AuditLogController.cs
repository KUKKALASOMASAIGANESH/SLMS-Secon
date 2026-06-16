using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

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
        var logs =
            await _service.GetAllAsync();

        return View(logs);
    }

    public async Task<IActionResult>
        Details(int id)
    {
        var log =
            await _service.GetByIdAsync(id);

        if (log == null)
            return NotFound();

        return View(log);
    }
}