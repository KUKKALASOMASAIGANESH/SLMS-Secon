using Microsoft.AspNetCore.Mvc;

using SLMS.WebApp.Models.Inventory;
using SLMS.WebApp.Services.Interfaces;

namespace SLMS.WebApp.Controllers;

public class ShelfController : Controller
{
    private readonly IShelfService _shelfService;

    public ShelfController(
        IShelfService shelfService)
    {
        _shelfService = shelfService;
    }

    // List Shelves
    public async Task<IActionResult>
    Index(string? searchTerm)
    {
        var shelves =
            await _shelfService.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            shelves = shelves
                .Where(x =>
                    x.ShelfName.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        ViewBag.SearchTerm =
            searchTerm;

        return View(shelves);
    }

    // Create Shelf
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>
Create(CreateShelfViewModel model)
    {
        if (model.CurrentBookCount > model.Capacity)
        {
            ModelState.AddModelError(
                "CurrentBookCount",
                "Current Book Count cannot exceed Capacity");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _shelfService
            .CreateAsync(model);

        TempData["SuccessMessage"] =
            "Shelf added successfully!";

        return RedirectToAction(nameof(Index));
    }

    // Details
    public async Task<IActionResult>
        Details(int id)
    {
        var shelf =
            await _shelfService
                .GetByIdAsync(id);

        if (shelf == null)
        {
            return NotFound();
        }

        return View(shelf);
    }

    // Edit
    public async Task<IActionResult>
        Edit(int id)
    {
        var shelf =
            await _shelfService
                .GetByIdAsync(id);

        if (shelf == null)
        {
            return NotFound();
        }

        var model =
            new UpdateShelfViewModel
            {
                ShelfName =
                    shelf.ShelfName,

                Capacity =
                    shelf.Capacity,

                CurrentBookCount =
                    shelf.CurrentBookCount
            };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult>
    Edit(
        int id,
        UpdateShelfViewModel model)
    {
        if (model.CurrentBookCount > model.Capacity)
        {
            ModelState.AddModelError(
                "CurrentBookCount",
                "Current Book Count cannot exceed Capacity");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _shelfService
            .UpdateAsync(id, model);

        TempData["SuccessMessage"] =
            "Shelf updated successfully!";

        return RedirectToAction(
            nameof(Index));
    }

    // Delete
    public async Task<IActionResult>
    Delete(int id)
    {
        try
        {
            await _shelfService
                .DeleteAsync(id);

            TempData["SuccessMessage"] =
                "Shelf deleted successfully";

            return RedirectToAction(
                nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["SuccessMessage"] =
                ex.Message;

            return RedirectToAction(
                nameof(Index));
        }
    }
}