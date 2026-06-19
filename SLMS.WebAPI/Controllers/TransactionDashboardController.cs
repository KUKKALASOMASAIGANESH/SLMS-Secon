using Microsoft.AspNetCore.Mvc;

using SLMS.BLL.Interfaces;

namespace SLMS.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionDashboardController
    : ControllerBase
{
    private readonly
        ITransactionDashboardService _service;

    public TransactionDashboardController(
        ITransactionDashboardService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult>
        GetDashboard()
    {
        return Ok(
            await _service.GetDashboardAsync());
    }
}