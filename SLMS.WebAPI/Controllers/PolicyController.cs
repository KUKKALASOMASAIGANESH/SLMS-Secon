using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;
using Microsoft.AspNetCore.Authorization;

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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
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

    [Authorize]
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