using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SLMS.BLL.Interfaces;

using SLMS.Shared.DTOs.Department;
using SLMS.Shared.Responses;

namespace SLMS.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentController(
        IDepartmentService service)
    {
        _service = service;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Department Controller Working");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _service.GetAllAsync();

        return Ok(
            new ApiResponse<
                IEnumerable<DepartmentResponseDto>>
            {
                Success = true,
                Message =
                    "Departments retrieved successfully",
                Data = result
            });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var result =
            await _service.GetByIdAsync(id);

        if (result == null)
        {
            return NotFound(
                new ApiResponse<object>
                {
                    Success = false,
                    Message =
                        "Department not found"
                });
        }

        return Ok(
            new ApiResponse<
                DepartmentResponseDto>
            {
                Success = true,
                Message =
                    "Department retrieved successfully",
                Data = result
            });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
<<<<<<< HEAD
        [FromBody] DepartmentCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);
=======
     Department department)
    {
        var departments =
            await _service.GetAllAsync();

        if (departments.Any(x =>
            x.DepartmentCode.ToLower() ==
            department.DepartmentCode.ToLower()))
        {
            return BadRequest(
                "Department Code already exists");
        }

        if (departments.Any(x =>
            x.DepartmentName.ToLower() ==
            department.DepartmentName.ToLower()))
        {
            return BadRequest(
                "Department Name already exists");
        }

        await _service.AddAsync(department);
>>>>>>> feature-custody

        return Ok(
            new ApiResponse<
                DepartmentResponseDto>
            {
                Success = true,
                Message =
                    "Department created successfully",
                Data = result
            });
    }
}