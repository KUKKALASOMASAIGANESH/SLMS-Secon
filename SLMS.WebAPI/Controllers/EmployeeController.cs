using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Employee;

namespace SLMS.WebAPI.Controllers;

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
        var employees =
            await _service.GetAllAsync();

        var result = employees.Select(e =>
            new EmployeeDto
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        var dto = new EmployeeDto
        {
            Id = data.Id,
            EmployeeNumber = data.EmployeeNumber,
            FullName = data.FullName,
            Email = data.Email,
            Phone = data.Phone,
            Designation = data.Designation,
            DepartmentId = data.DepartmentId
        };

        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult>
    Create(EmployeeCreateDto dto)
    {
        var employee = new Employee
        {
            EmployeeNumber = dto.EmployeeNumber,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Designation = dto.Designation,
            DepartmentId = dto.DepartmentId
        };

        await _service.AddAsync(employee);

        return Ok("Employee Created");
    }
    [HttpGet("search/{name}")]
    public async Task<IActionResult>
    Search(string name)
    {
        var data = await _service
            .SearchByNameAsync(name);

        var result = data.Select(e => new EmployeeDto
        {
            Id = e.Id,
            EmployeeNumber = e.EmployeeNumber,
            FullName = e.FullName,
            Email = e.Email,
            Designation = e.Designation,
            DepartmentId = e.DepartmentId
        });

        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult>
Update(EmployeeUpdateDto dto)
    {
        var employee = new Employee
        {
            Id = dto.Id,
            EmployeeNumber = dto.EmployeeNumber,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Designation = dto.Designation,
            DepartmentId = dto.DepartmentId
        };

        await _service.UpdateAsync(employee);

        return Ok("Employee Updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>
    Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok("Employee Deleted");
    }
}