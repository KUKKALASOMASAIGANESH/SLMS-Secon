using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService _service;

    public EmployeeController(
        EmployeeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var employees =
            await _service.GetAllAsync();

        return View(employees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        EmployeeViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result =
            await _service.CreateAsync(model);

        if (result)
            return RedirectToAction(nameof(Index));

        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var employee =
            await _service.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        return View(employee);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(
     EmployeeViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result =
            await _service.UpdateAsync(model);

        if (result)
            return RedirectToAction(nameof(Index));

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult>
Search(string name)
    {
        var employees =
            await _service.SearchAsync(name);

        return View("Index", employees);
    }
}