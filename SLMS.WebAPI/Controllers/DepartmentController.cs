using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.WebAPI.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
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
    public async Task<IActionResult> Create(
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

        return Ok("Department Created");
    }
}