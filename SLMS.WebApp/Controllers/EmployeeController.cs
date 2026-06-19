using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService _service;


public EmployeeController(EmployeeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var employees =
                await _service.GetAllAsync();

            return View(employees);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load employees.";

            return View(new List<EmployeeViewModel>());
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
        EmployeeViewModel model)
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
                    "Employee created successfully.";

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                error);

            return View(model);

            TempData["Error"] =
                "Unable to create employee.";

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
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var employee =
                await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to load employee.";

            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EmployeeViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var result =
                await _service.UpdateAsync(model);

            if (result)
            {
                TempData["Success"] =
                    "Employee updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] =
                "Unable to update employee.";

            return View(model);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "An unexpected error occurred.";

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            TempData["Success"] =
                "Employee deleted successfully.";
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Unable to delete employee.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Search(string name)
    {
        try
        {
            var employees =
                await _service.SearchAsync(name);

            return View("Index", employees);
        }
        catch (Exception)
        {
            TempData["Error"] =
                "Search operation failed.";

            return View("Index",
                new List<EmployeeViewModel>());
        }
    }


}
