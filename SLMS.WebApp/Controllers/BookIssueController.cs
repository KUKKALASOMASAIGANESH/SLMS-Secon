using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

public class BookIssueController : Controller
{
    private readonly BookIssueService _service;

    public BookIssueController(
        BookIssueService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var issues =
            await _service.GetAllAsync();

        return View(issues);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        BookIssueViewModel model)
    {
        Console.WriteLine("BOOK ISSUE CREATE CLICKED");

        Console.WriteLine($"InventoryItemId = {model.InventoryItemId}");
        Console.WriteLine($"EmployeeId = {model.EmployeeId}");
        Console.WriteLine($"IssuedByUserId = {model.IssuedByUserId}");

        try
        {
            await _service.CreateAsync(model);

            Console.WriteLine("BOOK ISSUE SAVED");

            TempData["SuccessMessage"] =
                "Book Issued Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            TempData["ErrorMessage"] =
                ex.Message;

            return View(model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var issue =
            await _service.GetByIdAsync(id);

        if (issue == null)
            return NotFound();

        return View(issue);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        BookIssueViewModel model)
    {
        try
        {
            await _service.UpdateAsync(model);

            TempData["SuccessMessage"] =
                "Book Issue Updated Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            TempData["ErrorMessage"] =
                ex.Message;

            return View(model);
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var issue =
            await _service.GetByIdAsync(id);

        if (issue == null)
            return NotFound();

        return View(issue);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            TempData["SuccessMessage"] =
                "Book Issue Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            TempData["ErrorMessage"] =
                ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }
}