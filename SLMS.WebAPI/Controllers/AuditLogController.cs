using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Custody;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
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
        var data = await _service.GetAllAsync();

        var result = data.Select(a => new AuditLogDto
        {
            Id = a.Id,
            UserId = a.UserId,
            Module = a.Module,
            Action = a.Action,
            OldValue = a.OldValue,
            NewValue = a.NewValue,
            IPAddress = a.IPAddress
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        var dto = new AuditLogDto
        {
            Id = data.Id,
            UserId = data.UserId,
            Module = data.Module,
            Action = data.Action,
            OldValue = data.OldValue,
            NewValue = data.NewValue,
            IPAddress = data.IPAddress
        };

        return Ok(dto);
    }
}