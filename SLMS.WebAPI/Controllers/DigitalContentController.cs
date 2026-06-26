using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin,Librarian,User")]
[ApiController]
[Route("api/[controller]")]
public class DigitalContentController : ControllerBase
{
    private readonly IDigitalContentService _service;
    private readonly IDigitalContentRequestService _requestService;
    private readonly IDownloadHistoryService _downloadHistoryService;

    public DigitalContentController(
    IDigitalContentService service,
    IDigitalContentRequestService requestService,
    IDownloadHistoryService downloadHistoryService)
    {
        _service = service;
        _requestService = requestService;
        _downloadHistoryService = downloadHistoryService;
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


    [HttpGet("access/{contentId}")]
    public async Task<IActionResult> HasAccess(int contentId)
    {
        int employeeId = 1; // TEMPORARY

        var request =
            (await _requestService.GetAllAsync())
            .FirstOrDefault(x =>
                x.EmployeeId == employeeId &&
                x.DigitalContentId == contentId &&
                x.ApprovalStatus == "Approved");

        return Ok(request != null);
    }

    [HttpGet("canaccess/{contentId}/{employeeId}")]
    public async Task<IActionResult> CanAccess(
    int contentId,
    int employeeId)
    {
        var requests =
            await _requestService.GetAllAsync();

        var approved =
            requests.Any(x =>
                x.DigitalContentId == contentId &&
                x.EmployeeId == employeeId &&
                x.ApprovalStatus == "Approved");

        return Ok(approved);
    }

    [HttpPost("download/{contentId}")]
    public async Task<IActionResult> DownloadContent(
    int contentId)
    {
        int employeeId = 1; // temporary

        var approvedRequest =
            (await _requestService.GetAllAsync())
            .FirstOrDefault(x =>
                x.EmployeeId == employeeId &&
                x.DigitalContentId == contentId &&
                x.ApprovalStatus == "Approved");

        if (approvedRequest == null)
        {
            return BadRequest(
                "Request not approved");
        }

        var history =
            new DownloadHistory
            {
                EmployeeId = employeeId,
                DigitalContentId = contentId,
                DownloadedOn = DateTime.UtcNow
            };

        await _downloadHistoryService.AddAsync(history);

        return Ok();
    }
}