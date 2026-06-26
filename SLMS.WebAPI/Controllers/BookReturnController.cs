using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.BookReturn;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin,Librarian")]
[Route("api/[controller]")]
[ApiController]
public class BookReturnController : ControllerBase
{
    private readonly IBookReturnService _service;

    public BookReturnController(IBookReturnService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> Create(BookReturnCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        BookReturnUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return Ok("Book Return Deleted");
    }
}