using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;

using SLMS.Shared.DTOs.Shelf;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin,Librarian")]
[ApiController]
[Route("api/[controller]")]
public class ShelfController : ControllerBase
{
    private readonly IShelfService _service;

    public ShelfController(
        IShelfService service)
    {
        _service = service;
    }

    // GET: api/Shelf
    [HttpGet]
    public async Task<IActionResult>
        GetAll()
    {
        var result =
            await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/Shelf/5
    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetById(int id)
    {
        var result =
            await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // POST: api/Shelf
    [HttpPost]
    public async Task<IActionResult>
        Create(
            ShelfCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    // PUT: api/Shelf/5
    // PUT: api/Shelf/5
    [HttpPut("{id}")]
    public async Task<IActionResult>
    Update(
        int id,
        ShelfUpdateDto dto)
    {
        try
        {
            var result =
                await _service.UpdateAsync(
                    id,
                    dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Shelf/5
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
                "Shelf Deleted Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}