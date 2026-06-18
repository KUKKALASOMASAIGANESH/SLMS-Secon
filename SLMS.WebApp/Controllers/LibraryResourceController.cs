using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

public class LibraryResourceController : Controller
{
    private readonly LibraryResourceService _service;
    private readonly CategoryService _categoryService;

    public LibraryResourceController(
        LibraryResourceService service,
        CategoryService categoryService)
    {
        _service = service;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var resources = await _service.GetAllAsync();
        return View(resources);
    }

    public async Task<IActionResult> Create()
    {
        var categories =
            await _categoryService.GetAllAsync();

        var model =
            new LibraryResourceViewModel();

        model.CategoryList =
            categories.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        LibraryResourceViewModel model)
    {
        try
        {
            await _service.CreateAsync(model);

            TempData["SuccessMessage"] =
                "Resource Added Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            var categories =
                await _categoryService.GetAllAsync();

            model.CategoryList =
                categories.Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();

            TempData["ErrorMessage"] =
                "Invalid Category Id. Please select a valid category.";

            return View(model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var resource =
            await _service.GetByIdAsync(id);

        if (resource == null)
        {
            return NotFound();
        }

        var categories =
            await _categoryService.GetAllAsync();

        resource.CategoryList =
            categories.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

        return View(resource);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        LibraryResourceViewModel model)
    {
        try
        {
            await _service.UpdateAsync(model);

            TempData["SuccessMessage"] =
                "Resource Updated Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            var categories =
                await _categoryService.GetAllAsync();

            model.CategoryList =
                categories.Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();

            TempData["ErrorMessage"] =
                "Invalid Category Id. Please select a valid category.";

            return View(model);
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var resource =
            await _service.GetByIdAsync(id);

        if (resource == null)
        {
            return NotFound();
        }

        return View(resource);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            TempData["SuccessMessage"] =
                "Resource Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            TempData["ErrorMessage"] =
                "Failed to delete resource";

            return RedirectToAction(nameof(Index));
        }
    }
}