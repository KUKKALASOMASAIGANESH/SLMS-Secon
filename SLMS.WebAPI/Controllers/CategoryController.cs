using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Category;

namespace SLMS.WebAPI.Controllers;


[Authorize(Roles = "Admin,Librarian")]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(
        ICategoryService service)
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
    public async Task<IActionResult> GetById(
        int id)
    {
        var data =
            await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CategoryCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        CategoryUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult>
    Delete(int id)
    {
        try
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok(
                "Category deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}