using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin,Librarian")]
[Route("api/reports")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly ReportService _service;

    public ReportController(ReportService service)
    {
        _service = service;
    }

    //[HttpGet("inventory")]
    //public async Task<IActionResult> Inventory()
     //   => Ok(await _service.GetInventoryReport());

    [HttpGet("issues")]
    public async Task<IActionResult> Issues()
        => Ok(await _service.GetIssueReport());

    [HttpGet("overdue")]
    public async Task<IActionResult> Overdue()
        => Ok(await _service.GetOverdueReport());

    [HttpGet("fine")]
    public async Task<IActionResult> Fine()
        => Ok(await _service.GetFineReport());
}