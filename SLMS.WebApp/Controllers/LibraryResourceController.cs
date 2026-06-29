using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;
using SLMS.WebApp.Services.Interfaces;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin,Librarian,User")]
public class LibraryResourceController : Controller
{
    private readonly LibraryResourceService _service;
    private readonly CategoryService _categoryService;
    private readonly IShelfService _shelfService;

    public LibraryResourceController(
        LibraryResourceService service,
        CategoryService categoryService,
        IShelfService shelfService)
    {
        _service = service;
        _categoryService = categoryService;
        _shelfService = shelfService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> Index(int page = 1)
    {
        try
        {
            var issues = await _service.GetAllAsync();

            int pageSize = 7;

            var data = issues
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;

            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    issues.Count / (double)pageSize);

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load book issues.";

            return View(new List<BookIssueViewModel>());
        }
    }
    [HttpGet]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> Details(int id)
    {
        var resource = await _service.GetByIdAsync(id);

        if (resource == null)
            return NotFound();

        return View(resource);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> Create()
    {
        var model = new LibraryResourceViewModel();
        await LoadDropdowns(model);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        LibraryResourceViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(model);
                return View(model);
            }

            await _service.CreateAsync(model);

            TempData["SuccessMessage"] =
                "Resource Added Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            await LoadDropdowns(model);

            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View(model);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> Edit(int id)
    {
        var resource = await _service.GetByIdAsync(id);

        if (resource == null)
            return NotFound();

        await LoadDropdowns(resource);

        return View(resource);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        LibraryResourceViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(model);
                return View(model);
            }

            await _service.UpdateAsync(model);

            TempData["SuccessMessage"] =
                "Resource Updated Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            await LoadDropdowns(model);

            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View(model);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            TempData["SuccessMessage"] =
                "Resource Deleted Successfully";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            TempData["ErrorMessage"] =
                "Failed to delete resource";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdowns(
        LibraryResourceViewModel model)
    {
        var categories =
            await _categoryService.GetAllAsync();

        var shelves =
            await _shelfService.GetAllAsync();

        model.CategoryList =
            categories.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

        model.Shelves =
            shelves.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ShelfName
                }).ToList();
    }
}