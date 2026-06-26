using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin,Librarian")]
public class CategoryController : Controller
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        try
        {
            var categories =
                await _service.GetAllAsync();

            int pageSize = 7;

            var data = categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;

            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    categories.Count / (double)pageSize);

            return View(data);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load categories.";

            return View(new List<CategoryViewModel>());
        }
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _service.CreateAsync(model);

            TempData["SuccessMessage"] =
                "Category Added Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            TempData["ErrorMessage"] =
                "Something went wrong while saving category.";

            return View(model);
        }
        finally
        {
            Console.WriteLine(
                $"Create Category Request Completed At {DateTime.Now}");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category =
            await _service.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        CategoryViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.UpdateAsync(model);

            TempData["SuccessMessage"] =
                "Category Updated Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            TempData["ErrorMessage"] =
                "Failed to update category";

            return View(model);
        }
    }
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            TempData["SuccessMessage"] =
                "Category Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] =
                ex.Message;

            return RedirectToAction(
                nameof(Index));
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var category =
            await _service.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        return View(category);
    }
}