using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;

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
        var result =
            await _service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result =
            await _service.GetByIdAsync(id);

<<<<<<< HEAD
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        EmployeeCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        EmployeeUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var result =
            await _service.DeleteAsync(id);

        if (!result)
            return NotFound();
=======
        if (data == null)
            return NotFound("Employee not found");

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
    public async Task<IActionResult> Create(
        EmployeeCreateDto dto)
    {
        try
        {
            var employees =
                await _service.GetAllAsync();

            if (employees.Any(x =>
                x.EmployeeNumber.ToLower() ==
                dto.EmployeeNumber.ToLower()))
            {
                return BadRequest(
                    "Employee Number already exists");
            }

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

            return Ok("Employee Created Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> Search(string name)
    {
        var data = await _service
            .SearchByNameAsync(name);

        var result = data.Select(e =>
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

    [HttpPut]
    public async Task<IActionResult> Update(
        EmployeeUpdateDto dto)
    {
        try
        {
            var employees =
                await _service.GetAllAsync();

            if (employees.Any(x =>
                x.Id != dto.Id &&
                x.EmployeeNumber.ToLower() ==
                dto.EmployeeNumber.ToLower()))
            {
                return BadRequest(
                    "Employee Number already exists");
            }

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

            return Ok("Employee Updated Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var employee =
                await _service.GetByIdAsync(id);
>>>>>>> feature-custody

            if (employee == null)
                return NotFound("Employee not found");

            await _service.DeleteAsync(id);

            return Ok("Employee Deleted Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}