using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models.Inventory;
using SLMS.WebApp.Services.Interfaces;

namespace SLMS.WebApp.Controllers;
// Handles Inventory Management UI operations.
// Communicates with InventoryService to perform
// CRUD operations through Web API endpoints.
public class InventoryController : Controller
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(
        IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }
    // Retrieves all inventory items and displays
    // them in the inventory management page.
    // Retrieves inventory items and supports searching.
    public async Task<IActionResult>
    Index(
        int page = 1,
        string? searchTerm = null,
        string? sortBy = null,
        bool descending = false)
    {
        const int pageSize = 10;

        var pagedData =
            await _inventoryService
                .GetPagedAsync(page, pageSize);

        var items = pagedData.Items;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            items = items
                .Where(x =>
                    x.AccessionNumber.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    x.InventoryNumber.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            items = sortBy switch
            {
                "AccessionNumber" =>
                    descending
                        ? items.OrderByDescending(
                            x => x.AccessionNumber)
                            .ToList()
                        : items.OrderBy(
                            x => x.AccessionNumber)
                            .ToList(),

                "InventoryNumber" =>
                    descending
                        ? items.OrderByDescending(
                            x => x.InventoryNumber)
                            .ToList()
                        : items.OrderBy(
                            x => x.InventoryNumber)
                            .ToList(),

                "ShelfNumber" =>
                    descending
                        ? items.OrderByDescending(
                            x => x.ShelfNumber)
                            .ToList()
                        : items.OrderBy(
                            x => x.ShelfNumber)
                            .ToList(),

                "Price" =>
                    descending
                        ? items.OrderByDescending(
                            x => x.Price)
                            .ToList()
                        : items.OrderBy(
                            x => x.Price)
                            .ToList(),

                _ => items
            };
        }

        var model =
            new InventoryPagedViewModel
            {
                Items = items,
                TotalCount = pagedData.TotalCount,
                Page = pagedData.Page,
                PageSize = pagedData.PageSize,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                Descending = descending
            };

        return View(model);
    }

    // Displays inventory summary dashboard
    public async Task<IActionResult>
        Summary()
    {
        var summary =
            await _inventoryService
                .GetSummaryAsync();

        return View(summary);
    }


    // Validates and submits a new inventory item
    // to the backend API.

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult>
    Create(CreateInventoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _inventoryService
            .CreateAsync(model);

        return RedirectToAction(nameof(Index));
    }

    // Retrieves an inventory item and loads it into the edit form
    public async Task<IActionResult> 
        Edit(int id)
    {
        var inventoryItem =
            await _inventoryService
                .GetByIdAsync(id);

        if (inventoryItem == null)
        {
            return NotFound();
        }

        var model =
            new UpdateInventoryViewModel
            {

                ResourceId =
                    inventoryItem.ResourceId,

                AccessionNumber =
                    inventoryItem.AccessionNumber,

                InventoryNumber =
                    inventoryItem.InventoryNumber,

                ShelfNumber =
                    inventoryItem.ShelfNumber,

                Price =
                    inventoryItem.Price
            };

        return View(model);
    }

    [HttpPost]

    // Updates inventory information through the API
    public async Task<IActionResult>
    Edit(
        int id,
        UpdateInventoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _inventoryService
            .UpdateAsync(id, model);

        return RedirectToAction(nameof(Index));
    }

    // Retrieves and displays detailed information
    // for a specific inventory item
    public async Task<IActionResult>
    Details(int id)
    {
        var inventoryItem =
            await _inventoryService
                .GetByIdAsync(id);

        if (inventoryItem == null)
        {
            return NotFound();
        }

        return View(inventoryItem);
    }

    // Deletes the selected inventory item
    // and redirects back to the inventory list
    // Deletes the selected inventory item
    public async Task<IActionResult>
        Delete(int id)
    {
        await _inventoryService
            .DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

  

    

    // Displays inventory dashboard
    public async Task<IActionResult>
        Dashboard()
    {
        var model =
            new InventoryDashboardViewModel
            {
                Summary =
                    await _inventoryService
                        .GetSummaryAsync(),

                CostReport =
                    await _inventoryService
                        .GetCostReportAsync(),

                ResourceReport =
                    await _inventoryService
                        .GetResourceReportAsync(),

                AvailabilityReport =
                    await _inventoryService
                        .GetAvailabilityReportAsync(),

                ShelfReport =
                    await _inventoryService
                        .GetShelfReportAsync(),
            };

        return View(model);
    }
    // Displays inactive inventory items
    public async Task<IActionResult>
        Inactive()
    {
        var items =
            await _inventoryService
                .GetInactiveAsync();

        return View(items);
    }

    // Restores an inactive inventory item
    public async Task<IActionResult>
        Restore(int id)
    {
        await _inventoryService
            .RestoreAsync(id);

        return RedirectToAction(
            nameof(Inactive));
    }

}