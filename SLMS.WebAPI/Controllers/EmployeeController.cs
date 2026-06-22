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
<<<<<<< HEAD

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
=======
    [HttpPut]
    public async Task<IActionResult> Update(
    EmployeeUpdateDto dto)
    {
        try
        {
            var employee =
                await _service.GetByIdAsync(dto.Id);

            if (employee == null)
            {
                return NotFound(
                    "Employee not found");
            }

            // TEMPORARILY REMOVE DUPLICATE CHECK

            employee.EmployeeNumber =
                dto.EmployeeNumber;

            employee.FullName =
                dto.FullName;

            employee.Email =
                dto.Email;

            employee.Phone =
                dto.Phone;

            employee.Designation =
                dto.Designation;

            employee.DepartmentId =
                dto.DepartmentId;


            await _service.UpdateAsync(employee);

            return Ok(
                "Employee Updated Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
>>>>>>> origin/feature-custody
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
}