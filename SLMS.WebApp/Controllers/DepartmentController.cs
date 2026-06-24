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

    public async Task<IActionResult> Index()
    {
        try
        {
            var data =
                await _service.GetAllAsync();

            return View(data);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.ToString();

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
}