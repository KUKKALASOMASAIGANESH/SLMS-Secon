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
        var data =
            await _service.GetAllAsync();

        return View(data);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
     CustodyHistoryViewModel model)
    {
        var result =
            await _service.CreateAsync(model);

        if (result)
            return RedirectToAction(nameof(Index));

        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult>
    SearchByInventory(int inventoryItemId)
    {
        var data = await _service
            .GetByInventoryItemAsync(inventoryItemId);

        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult>
    CurrentCustodian(int inventoryItemId)
    {
        var data = await _service
            .GetCurrentCustodianAsync(inventoryItemId);

        if (data == null)
            return NotFound();

        return View(data);
    }

}