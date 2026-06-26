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

    // Admin, Librarian, User can view all resources
    [HttpGet]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // Admin, Librarian, User can view details
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // Admin, Librarian, User can search
    [HttpGet("search/{keyword}")]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> Search(string keyword)
    {
        var result = await _service.SearchAsync(keyword);
        return Ok(result);
    }

    [HttpGet("books")]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> GetBooks()
    {
        var resources = await _service.GetAllAsync();

        return Ok(resources.Where(x =>
            x.ResourceType == "Book"));
    }

    [HttpGet("journals")]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> GetJournals()
    {
        var resources = await _service.GetAllAsync();

        return Ok(resources.Where(x =>
            x.ResourceType == "Journal"));
    }

    [HttpGet("magazines")]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> GetMagazines()
    {
        var resources = await _service.GetAllAsync();

        return Ok(resources.Where(x =>
            x.ResourceType == "Magazine"));
    }

    // Only Admin and Librarian can create
    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> Create(
        LibraryResourceCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Only Admin and Librarian can update
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> Update(
        int id,
        LibraryResourceUpdateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Only Admin and Librarian can delete
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok("Library Resource Deleted");
    }
}