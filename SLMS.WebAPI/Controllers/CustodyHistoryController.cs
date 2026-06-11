using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustodyHistoryController : ControllerBase
{
    private readonly ICustodyHistoryService _service;

    public CustodyHistoryController(
        ICustodyHistoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CustodyHistory custodyHistory)
    {
        await _service.AddAsync(custodyHistory);

        return Ok("Custody History Created");
    }
    [HttpGet("inventory/{inventoryItemId}")]
    public async Task<IActionResult>
    GetByInventoryItem(int inventoryItemId)
    {
        var data = await _service
            .GetByInventoryItemAsync(inventoryItemId);

        return Ok(data);
    }
    [HttpGet("current/{inventoryItemId}")]
    public async Task<IActionResult>
    GetCurrentCustodian(int inventoryItemId)
    {
        var data = await _service
            .GetCurrentCustodianAsync(inventoryItemId);

        if (data == null)
            return NotFound();

        return Ok(data);
    }
}