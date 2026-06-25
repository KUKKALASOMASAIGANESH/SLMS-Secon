using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;

using SLMS.Shared.DTOs.LibraryResource;

namespace SLMS.WebAPI.Controllers;

[Authorize]
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
    public async Task<IActionResult> Search(
        string keyword)
    {
        var result =
            await _service.SearchAsync(keyword);

        return Ok(result);
    }

    // BOOKS

    [HttpGet("books")]
    public async Task<IActionResult> GetBooks()
    {
        var resources =
            await _service.GetAllAsync();

        return Ok(
            resources.Where(x =>
                x.ResourceType == "Book"));
    }

    // JOURNALS

    [HttpGet("journals")]
    public async Task<IActionResult> GetJournals()
    {
        var resources =
            await _service.GetAllAsync();

        return Ok(
            resources.Where(x =>
                x.ResourceType == "Journal"));
    }

    // MAGAZINES

    [HttpGet("magazines")]
    public async Task<IActionResult> GetMagazines()
    {
        var resources =
            await _service.GetAllAsync();

        return Ok(
            resources.Where(x =>
                x.ResourceType == "Magazine"));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    LibraryResourceCreateDto dto)
    {
        try
        {
            Console.WriteLine("API Create Started");

            var result =
                await _service.CreateAsync(dto);

            Console.WriteLine("API Create Success");

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
     LibraryResourceUpdateDto dto)
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

        return Ok("Library Resource Deleted");
    }
}