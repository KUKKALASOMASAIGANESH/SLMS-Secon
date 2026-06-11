using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;

using SLMS.Shared.DTOs.LibraryResource;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryResourceController : ControllerBase
{
    private readonly ILibraryResourceService _service;

    public LibraryResourceController(
        ILibraryResourceService service)
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
    public async Task<IActionResult> GetById(
        int id)
    {
        var result =
            await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpGet("search/{keyword}")]
    public async Task<IActionResult>
    Search(string keyword)
    {
        var result =
            await _service.SearchAsync(keyword);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        LibraryResourceCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    LibraryResourceUpdateDto dto)
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

        return Ok("Library Resource Deleted");
    }
}