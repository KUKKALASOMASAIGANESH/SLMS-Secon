using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DigitalContentRequestController
    : ControllerBase
{
    private readonly IDigitalContentRequestService _service;

    public DigitalContentRequestController(
        IDigitalContentRequestService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    DigitalContentRequestCreateDto dto)
    {
        var request = new DigitalContentRequest
        {
            EmployeeId = dto.EmployeeId,

            DigitalContentId = dto.DigitalContentId,

            ApprovalStatus = "Pending",

            RequestDate = DateTime.UtcNow
        };

        await _service.AddAsync(request);

        return Ok(request);
    }
}