using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.DigitalLibrary;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SLMS.DAL.Repositories.Interfaces;

namespace SLMS.WebAPI.Controllers;


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
        return Ok(await _service.GetAllAsync());
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
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId =
            int.Parse(userIdClaim.Value);

        var user =
            await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return Unauthorized();
        }

        var request = new DigitalContentRequest
        {
            EmployeeId = user.EmployeeId,

            DigitalContentId = dto.DigitalContentId,

            ApprovalStatus = "Pending",

            RequestDate = DateTime.UtcNow
        };

        Console.WriteLine($"EmployeeId = {user.EmployeeId}");
        Console.WriteLine($"DigitalContentId = {dto.DigitalContentId}");
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
}