using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin,Librarian")]
public class CustodyHistoryController : Controller
{
    private readonly CustodyHistoryService _service;
    private readonly DepartmentService _departmentService;

    public CustodyHistoryController(
        CustodyHistoryService service,
        DepartmentService departmentService)
    {
        _service = service;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var data = await _service.GetReportAsync();

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] = "Unable to load custody history.";

            return View(new List<CustodyHistoryReportViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new CustodyHistoryViewModel();

        var departments = await _departmentService.GetAllAsync();

        model.Departments = departments.Select(d =>
            new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.DepartmentName
            }).ToList();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustodyHistoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.GetAllAsync();

                model.Departments = departments.Select(d =>
                    new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.DepartmentName
                    }).ToList();

                return View(model);
            }

            var result = await _service.CreateAsync(model);

            if (result)
            {
                TempData["Success"] = "Custody record created successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Unable to create custody record.";
            return View(model);
        }
        catch (Exception)
        {
            TempData["Error"] = "An unexpected error occurred.";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] = "Unable to load custody details.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> SearchByInventory(int inventoryItemId)
    {
        try
        {
            var data = await _service.GetByInventoryItemAsync(inventoryItemId);

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] = "Search operation failed.";

            return View(new List<CustodyHistoryViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> CurrentCustodian(int inventoryItemId)
    {
        try
        {
            var data = await _service.GetCurrentCustodianAsync(inventoryItemId);

            if (data == null)
                return NotFound();

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] = "Unable to load current custodian.";
            return RedirectToAction(nameof(Index));
        }
    }
}