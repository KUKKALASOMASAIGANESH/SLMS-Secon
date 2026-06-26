using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SLMS.DAL.Repositories.Interfaces;

namespace SLMS.WebAPI.Controllers;

[Authorize(Roles = "Admin,Librarian,User")]
[ApiController]
[Route("api/[controller]")]
public class DigitalContentRequestController
    : ControllerBase
{
    private readonly IDigitalContentRequestService _service;
    private readonly IUserRepository _userRepository;

    public DigitalContentRequestController(
    IDigitalContentRequestService service,
    IUserRepository userRepository)
    {
        _service = service;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var requests =
     await _service.GetAllWithDetailsAsync();

        var result = requests.Select(x =>
            new DigitalContentRequestResponseDto
            {
                Id = x.Id,

                EmployeeId = x.EmployeeId,

                DigitalContentId = x.DigitalContentId,

                EmployeeName =
                    x.Employee?.FullName ?? "",

                ContentTitle =
                    x.DigitalContent?.Title ?? "",

                RequestDate = x.RequestDate,

                ApprovalStatus = x.ApprovalStatus
            });

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
DigitalContentRequestCreateDto dto)
    {
        var request = new DigitalContentRequest
        {
            EmployeeId = 1, // temporary

            DigitalContentId = dto.DigitalContentId,

            ApprovalStatus = "Pending",

            RequestDate = DateTime.UtcNow
        };

        await _service.AddAsync(request);

        return Ok(request);
    }

    [HttpPut("approve/{id}")]
    public async Task<IActionResult> Approve(int id)
    {
        var request =
            await _service.GetByIdAsync(id);

        if (request == null)
            return NotFound();

        request.ApprovalStatus = "Approved";

        await _service.UpdateAsync(request);

        return Ok("Request Approved");
    }

    [HttpPut("reject/{id}")]
    public async Task<IActionResult> Reject(int id)
    {
        var request =
            await _service.GetByIdAsync(id);

        if (request == null)
            return NotFound();

        request.ApprovalStatus = "Rejected";

        await _service.UpdateAsync(request);

        return Ok("Request Rejected");
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult>
GetByEmployee(int employeeId)
    {
        var requests =
            await _service.GetByEmployeeIdAsync(employeeId);

        var result = requests.Select(x => new
        {
            Id = x.Id,
            DigitalContentId = x.DigitalContentId,
            ApprovalStatus = x.ApprovalStatus
        });

        return Ok(result);
    }
}