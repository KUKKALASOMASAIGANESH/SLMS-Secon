using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolicyController : ControllerBase
{
    private readonly IPolicyService _service;

    public PolicyController(
        IPolicyService service)
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
    PolicyCreateDto dto)
    {
        var policy = new Policy
        {
            Title = dto.PolicyTitle,

            Description = dto.PolicyContent,

            EffectiveDate = DateTime.UtcNow
        };

        await _service.AddAsync(policy);

        return Ok(policy);
    }
}