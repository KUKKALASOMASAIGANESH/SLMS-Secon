using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Employee;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin,Librarian")]
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(
        IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result =
            await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        EmployeeCreateDto dto)
    {
        try
        {
            var result =
                await _service.CreateAsync(dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        EmployeeUpdateDto dto)
    {
        try
        {
            var result =
                await _service.UpdateAsync(id, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var result =
            await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok(
            "Employee Deleted Successfully");
    }
    // NEW SEARCH API
    [HttpGet("search/{name}")]
    public async Task<IActionResult> Search(string name)
    {
        var employees =
            await _service.SearchByNameAsync(name);

        var result = employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            EmployeeNumber = e.EmployeeNumber,
            FullName = e.FullName,
            Email = e.Email,
            Phone = e.Phone,
            Designation = e.Designation,
            DepartmentId = e.DepartmentId
        });

        return Ok(result);
    }

}