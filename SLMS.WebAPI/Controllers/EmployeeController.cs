using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;

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
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        await _service.AddAsync(employee);

        return Ok("Employee Created");
    }
    [HttpGet("search/{name}")]
    public async Task<IActionResult>
    Search(string name)
    {
        var data = await _service
            .SearchByNameAsync(name);

        return Ok(data);
    }
    [HttpPut]
    public async Task<IActionResult> Update(Employee employee)
    {
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