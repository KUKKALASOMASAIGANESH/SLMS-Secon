using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DigitalContentController : ControllerBase
{
    private readonly IDigitalContentService _service;

    public DigitalContentController(
        IDigitalContentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
     DigitalContentCreateDto dto)
    {
        var digitalContent = new DigitalContent
        {
            Title = dto.Title,
            Description = dto.Description ?? string.Empty,
            Category = dto.ContentType,
            FilePath = dto.FilePath,

            // Temporary value until User module is integrated
            UploadedByUserId = 1
        };

        await _service.AddAsync(digitalContent);

        return Ok(digitalContent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    DigitalContentUpdateDto dto)
    {
        var content =
            await _service.GetByIdAsync(id);

        if (content == null)
            return NotFound();

        content.Title = dto.Title;
        content.Description = dto.Description;
        content.Category = dto.ContentType;
        content.FilePath = dto.FilePath;

        await _service.UpdateAsync(content);

        return Ok(content);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var content = await _service.GetByIdAsync(id);

        if (content == null)
        {
            return NotFound("Digital Content not found");
        }

        await _service.DeleteAsync(id);

        return Ok("Content Deleted Successfully");
    }
}