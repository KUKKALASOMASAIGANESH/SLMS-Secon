using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Custody;

namespace SLMS.WebAPI.Controllers;


[Authorize(Roles = "Admin,Librarian")]
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
        var result = data.Select(c => new CustodyHistoryDto
        {
            Id = c.Id,
            InventoryItemId = c.InventoryItemId,

            FromDepartmentId = c.FromDepartmentId,
            ToDepartmentId = c.ToDepartmentId,

            FromDepartmentName =
                c.FromDepartment?.DepartmentName ?? "",

            ToDepartmentName =
                c.ToDepartment?.DepartmentName ?? "",

            TransferDate = c.TransferDate,
            TransferReason = c.TransferReason,
            Remarks = c.Remarks,
            TransferredByUserId = c.TransferredByUserId
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        var dto = new CustodyHistoryDto
        {
            Id = data.Id,
            InventoryItemId = data.InventoryItemId,
            FromDepartmentId = data.FromDepartmentId,
            ToDepartmentId = data.ToDepartmentId,
            TransferDate = data.TransferDate,
            TransferReason = data.TransferReason,
            Remarks = data.Remarks,
            TransferredByUserId = data.TransferredByUserId
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
     CustodyHistoryCreateDto dto)
    {
        var custody = new CustodyHistory
        {
            InventoryItemId = dto.InventoryItemId,
            FromDepartmentId = dto.FromDepartmentId,
            ToDepartmentId = dto.ToDepartmentId,
            TransferDate = DateTime.UtcNow,
            TransferReason = dto.TransferReason,
            Remarks = dto.Remarks,
            TransferredByUserId = dto.TransferredByUserId
        };

        await _service.AddAsync(custody);

        return Ok("Custody Created");
    }

    [HttpGet("inventory/{inventoryItemId}")]
    public async Task<IActionResult> GetByInventoryItem(int inventoryItemId)
    {
        var data = await _service
            .GetByInventoryItemAsync(inventoryItemId);

        var result = data.Select(c => new CustodyHistoryDto
        {
            Id = c.Id,
            InventoryItemId = c.InventoryItemId,

            FromDepartmentId = c.FromDepartmentId,
            ToDepartmentId = c.ToDepartmentId,

            FromDepartmentName =
        c.FromDepartment?.DepartmentName ?? "",

            ToDepartmentName =
        c.ToDepartment?.DepartmentName ?? "",

            TransferDate = c.TransferDate,
            TransferReason = c.TransferReason,
            Remarks = c.Remarks,
            TransferredByUserId = c.TransferredByUserId
        });
    

        return Ok(result);
    }

    [HttpGet("current/{inventoryItemId}")]
    public async Task<IActionResult> GetCurrentCustodian(int inventoryItemId)
    {
        var data = await _service
            .GetCurrentCustodianAsync(inventoryItemId);

        if (data == null)
            return NotFound();

        return Ok(data);
    }
    [HttpGet("report")]
    public async Task<IActionResult> GetCustodyReport()
    {
        var result =
            await _service.GetCustodyReportAsync();

        return Ok(result);
    }
}