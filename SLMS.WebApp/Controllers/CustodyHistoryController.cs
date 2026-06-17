using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

public class CustodyHistoryController : Controller
{
    private readonly CustodyHistoryService _service;

    public CustodyHistoryController(
        CustodyHistoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var data =
                await _service.GetAllAsync();

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load custody records.";

            return View(
                new List<CustodyHistoryViewModel>());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CustodyHistoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var result =
                await _service.CreateAsync(model);

            if (result)
            {
                TempData["Success"] =
                    "Custody record created successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] =
                "Unable to create custody record.";

            return View(model);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "An unexpected error occurred.";

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var data =
                await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load custody details.";

            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult>
        SearchByInventory(int inventoryItemId)
    {
        try
        {
            var data = await _service
                .GetByInventoryItemAsync(inventoryItemId);

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Search operation failed.";

            return View(
                new List<CustodyHistoryViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult>
        CurrentCustodian(int inventoryItemId)
    {
        try
        {
            var data = await _service
                .GetCurrentCustodianAsync(inventoryItemId);

            if (data == null)
                return NotFound();

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load current custodian.";

            return RedirectToAction(nameof(Index));
        }
    }
}