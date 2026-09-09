using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Department;
using SLMS.Shared.Responses;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
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
        var result = await _service.GetAllAsync();

        return Ok(
            new ApiResponse<IEnumerable<DepartmentResponseDto>>
            {
                Success = true,
                Message = "Departments retrieved successfully",
                Data = result
            });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
        {
            return NotFound(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = "Department not found"
                });
        }

        return Ok(
            new ApiResponse<DepartmentResponseDto>
            {
                Success = true,
                Message = "Department retrieved successfully",
                Data = result
            });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] DepartmentCreateDto dto)
    {
        try
        {
            var result =
                await _service.CreateAsync(dto);

            return Ok(
                new ApiResponse<DepartmentResponseDto>
                {
                    Success = true,
                    Message = "Department created successfully",
                    Data = result
                });
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] DepartmentUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(id, dto);

        if (result == null)
        {
            return NotFound(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = "Department not found"
                });
        }

        return Ok(
            new ApiResponse<DepartmentResponseDto>
            {
                Success = true,
                Message = "Department updated successfully",
                Data = result
            });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _service.DeleteAsync(id);

        if (!result)
        {
            return NotFound(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = "Department not found"
                });
        }

        return Ok(
            new ApiResponse<object>
            {
                Success = true,
                Message = "Department deleted successfully"
            });
    }
    [HttpGet("search/{keyword}")]
    public async Task<IActionResult> Search(string keyword)
    {
        var result =
            await _service.SearchAsync(keyword);

        return Ok(new ApiResponse<IEnumerable<DepartmentResponseDto>>
        {
            Success = true,
            Message = "Departments retrieved successfully",
            Data = result
        });
    }
}