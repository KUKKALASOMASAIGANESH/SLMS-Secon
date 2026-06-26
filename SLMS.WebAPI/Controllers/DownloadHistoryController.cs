using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;
using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin,Librarian,User")]
[ApiController]
[Route("api/[controller]")]
public class DownloadHistoryController
    : ControllerBase
{
    private readonly IDownloadHistoryService _service;

    public DownloadHistoryController(
        IDownloadHistoryService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _service.GetAllWithContentAsync());
    }

    [AllowAnonymous]
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
    DownloadHistoryCreateDto dto)
    {
        var history = new DownloadHistory
        {
            EmployeeId = dto.EmployeeId,
            DigitalContentId = dto.DigitalContentId,
            DownloadedOn = DateTime.UtcNow
        };

        await _service.AddAsync(history);

        return Ok(history);
    }
}