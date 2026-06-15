using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogService _service;

    public AuditLogController(
        IAuditLogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetById(int id)
    {
        var data =
            await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return Ok(data);
    }
}