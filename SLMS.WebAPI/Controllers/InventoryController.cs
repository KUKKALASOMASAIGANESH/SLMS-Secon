/*
 * InventoryController
 *
 * Purpose:
 * Exposes REST API endpoints for inventory management.
 *
 * Responsibilities:
 * - Receive HTTP requests
 * - Validate request models
 * - Invoke InventoryService methods
 * - Return standardized API responses
 *
 * API Features:
 * - CRUD Operations
 * - Search and Filtering
 * - Pagination
 * - Sorting
 * - Shelf Management
 * - Availability Tracking
 * - Inventory Reports
 * - Soft Delete and Restore
 *
 * Flow:
 * Client/Swagger/Postman
 *    ↓
 * InventoryController
 *    ↓
 * InventoryService
 *    ↓
 * InventoryRepository
 *    ↓
 * PostgreSQL Database
 */


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Inventory;
using SLMS.Shared.Responses;

namespace SLMS.WebAPI.Controllers;

// REST API controller for managing inventory items,
// inventory reporting, shelf tracking and availability operations.
[Authorize(Roles = "Admin,Librarian")]
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


    // Retrieves all active inventory items
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


    // Search inventory items using filters such as
    // accession number, inventory number, shelf, title,
    // author, publisher and price range
    [HttpGet("search")]
    public async Task<IActionResult> Search(
    [FromQuery] InventorySearchDto searchDto)
    {
        var result =
            await _inventoryService.SearchAsync(searchDto);

        return Ok(result);
    }


    // Retrieves inventory items available on a specific shelf
    [HttpGet("shelf/{shelfNumber}")]
    public async Task<IActionResult>
    GetByShelf(string shelfNumber)
    {
        var result =
            await _inventoryService
                .GetByShelfAsync(shelfNumber);

        return Ok(result);
    }

    // Generates shelf-wise inventory summary report
    [HttpGet("report/shelf")]
    public async Task<IActionResult>
    GetShelfSummary()
    {
        var result =
            await _inventoryService
                .GetShelfSummaryAsync();

        return Ok(result);
    }

    // Updates inventory details for an existing item
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


    // Updates inventory details for an existing item
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


    // Performs soft delete by marking the inventory item inactive
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

    // Inventory reporting endpoints
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

    // Returns paginated inventory data
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

    // Returns inventory items sorted by supported fields
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

    // Availability tracking endpoints
    [HttpGet("available")]
    public async Task<IActionResult>
    GetAvailableBooks()
    {
        var result =
            await _inventoryService
                .GetAvailableBooksAsync();

        return Ok(result);
    }

    [HttpGet("unavailable")]
    public async Task<IActionResult>
    GetUnavailableBooks()
    {
        var result =
            await _inventoryService
                .GetUnavailableBooksAsync();

        return Ok(result);
    }

    [HttpGet("report/availability")]
    public async Task<IActionResult>
    GetAvailabilityReport()
    {
        var result =
            await _inventoryService
                .GetAvailabilityReportAsync();

        return Ok(result);
    }

    // Restores a previously soft deleted inventory item
    [HttpPut("restore/{id}")]
    public async Task<IActionResult> Restore(int id)
    {
        var restored =
            await _inventoryService
                .RestoreAsync(id);

        if (!restored)
            return NotFound();

        return Ok(
            "Inventory Item Restored Successfully");
    }

    // Retrieves all inactive (soft deleted) inventory items
    [HttpGet("inactive")]
    public async Task<IActionResult>
    GetInactive()
    {
        var result =
            await _inventoryService
                .GetInactiveAsync();

        return Ok(result);
    }
}