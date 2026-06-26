using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;
using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin,Librarian")]
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    PolicyCreateDto dto)
    {
        var policy =
            await _service.GetByIdAsync(id);

        if (policy == null)
            return NotFound();

        policy.Title =
            dto.PolicyTitle;

        policy.Description =
            dto.PolicyContent;

        await _service.UpdateAsync(policy);

        return Ok(policy);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var content = await _service.GetByIdAsync(id);

        if (content == null)
        {
            return NotFound("Policy  not found");
        }

        await _service.DeleteAsync(id);

        return Ok("Policy Deleted Successfully");
    }
}