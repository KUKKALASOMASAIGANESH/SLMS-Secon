using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.RolePermission;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class RolePermissionController
    : ControllerBase
{
    private readonly IRolePermissionService _service;

    public RolePermissionController(
        IRolePermissionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        RolePermissionCreateDto dto)
    {
        return Ok(
            await _service.CreateAsync(dto));
    }
}