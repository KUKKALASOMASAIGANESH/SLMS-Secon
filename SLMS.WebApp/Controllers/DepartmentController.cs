using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class DepartmentController : Controller
{
    private readonly DepartmentService _service;

    public DepartmentController(
        DepartmentService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        try
        {
            var departments =
                await _service.GetAllAsync();

            int pageSize = 5;

            var data = departments
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;

            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    departments.Count / (double)pageSize);

            return View(data);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;

            return View(new List<DepartmentViewModel>());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>
        Create(DepartmentViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var error =
     await _service.CreateAsync(model);

            if (error == null)
            {
                TempData["Success"] =
                    "Department created successfully.";

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                error);

            return View(model);
        }
        catch
        {
            TempData["Error"] =
                "Unexpected error occurred.";

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult>
        Details(int id)
    {
        var department =
            await _service.GetByIdAsync(id);

        if (department == null)
            return NotFound();

        return View(department);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result =
                await _service.DeleteAsync(id);

            if (result)
            {
                TempData["Success"] =
                    "Department deleted successfully.";
            }
            else
            {
                TempData["Error"] =
                    "Department not found.";
            }
        }
        catch
        {
            TempData["Error"] =
                "Unable to delete department.";
        }

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    DepartmentViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var error =
                await _service.UpdateAsync(model);

            if (error == null)
            {
                TempData["Success"] =
                    "Department updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                error);

            return View(model);
        }
        catch
        {
            TempData["Error"] =
                "Unable to update department.";

            return View(model);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var department =
            await _service.GetByIdAsync(id);

        if (department == null)
            return NotFound();

        return View(department);
    }
    [HttpGet]
    public async Task<IActionResult> Search(
    string keyword,
    int page = 1)
    {
        try
        {
            var departments =
                await _service.SearchAsync(keyword);

            int pageSize = 7;

            var data = departments
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    departments.Count / (double)pageSize);

            ViewBag.Search = keyword;

            return View("Index", data);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;

            return View("Index",
                new List<DepartmentViewModel>());
        }
    }
}