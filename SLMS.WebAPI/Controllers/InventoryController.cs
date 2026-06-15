
using SLMS.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(
        IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _inventoryService.GetAllAsync();

        return Ok(new ApiResponse<IEnumerable<InventoryItemDto>>
        {
            Success = true,
            Message = "Inventory items retrieved successfully",
            Data = result
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
    [FromQuery] InventorySearchDto searchDto)
    {
        var result =
            await _inventoryService.SearchAsync(searchDto);

        return Ok(result);
    }

    [HttpGet("shelf/{shelfNumber}")]
    public async Task<IActionResult>
    GetByShelf(string shelfNumber)
    {
        var result =
            await _inventoryService
                .GetByShelfAsync(shelfNumber);

        return Ok(result);
    }

    [HttpGet("report/shelf")]
    public async Task<IActionResult>
    GetShelfSummary()
    {
        var result =
            await _inventoryService
                .GetShelfSummaryAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    CreateInventoryItemDto dto)
    {
        await _inventoryService.CreateAsync(dto);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Inventory Item Created Successfully"
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var inventoryItem = await _inventoryService.GetByIdAsync(id);

        if (inventoryItem == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Inventory Item Not Found"
            });
        }

        return Ok(new ApiResponse<InventoryItemDto>
        {
            Success = true,
            Message = "Inventory Item Retrieved Successfully",
            Data = inventoryItem
        });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateInventoryItemDto dto)
    {
        var updated =
            await _inventoryService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Inventory Item Not Found"
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Inventory Item Updated Successfully"
        });
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _inventoryService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Inventory Item Not Found"
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Inventory Item Deleted Successfully"
        });
    }

    [HttpGet("report/summary")]
    public async Task<IActionResult>
    GetInventorySummary()
    {
        var result =
            await _inventoryService
                .GetInventorySummaryAsync();

        return Ok(result);
    }

    [HttpGet("report/resource")]
    public async Task<IActionResult>
    GetResourceInventoryReport()
    {
        var result =
            await _inventoryService
                .GetResourceInventoryReportAsync();

        return Ok(result);
    }

    [HttpGet("report/cost")]
    public async Task<IActionResult>
    GetInventoryCostReport()
    {
        var result =
            await _inventoryService
                .GetInventoryCostReportAsync();

        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(
    [FromQuery]
    InventoryPaginationDto paginationDto)
    {
        var result =
            await _inventoryService
                .GetPagedAsync(
                    paginationDto);

        return Ok(result);
    }

    [HttpGet("sorted")]
    public async Task<IActionResult>
    GetSorted(
        [FromQuery]
        InventorySortDto sortDto)
    {
        var result =
            await _inventoryService
                .GetSortedAsync(sortDto);

        return Ok(result);
    }
}