using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;

using SLMS.Shared.DTOs.BookIssue;

namespace SLMS.WebAPI.Controllers;


[Authorize(Roles = "Admin,Librarian")]
[Route("api/[controller]")]
[ApiController]
public class BookIssueController : ControllerBase
{
    private readonly IBookIssueService _service;

    public BookIssueController(
        IBookIssueService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result =
            await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpGet("overdue")]
    public async Task<IActionResult>
    GetOverdueBooks()
    {
        return Ok(
            await _service.GetOverdueBooksAsync());
    }

    [HttpPost]
    public async Task<IActionResult>
        Create(BookIssueCreateDto dto)
    {
        return Ok(
            await _service.CreateAsync(dto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult>
        Update(
            int id,
            BookIssueUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(
                id,
                dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>
        Delete(int id)
    {
        var deleted =
            await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return Ok(
            "Book Issue Deleted");
    }
    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult>
GetIssuedBooksByEmployee(int employeeId)
    {
        var books =
            await _service.GetAllAsync();

        var result =
            books.Where(x =>
                x.EmployeeId == employeeId &&
                x.Status == "Issued");

        return Ok(result);
    }
}