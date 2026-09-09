using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin")]

public class EmployeeController : Controller
{
    private readonly EmployeeService _service;
    private readonly DepartmentService _departmentService;


    public EmployeeController(
        EmployeeService service,
        DepartmentService departmentService)
    {
        _service = service;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index(int? departmentId, int page = 1)
    {
        var employees = await _service.GetAllAsync();

        var departments = await _departmentService.GetAllAsync();
        foreach (var employee in employees)
        {
            employee.DepartmentName = departments
                .FirstOrDefault(d => d.Id == employee.DepartmentId)
                ?.DepartmentName;
        }

        ViewBag.Departments = new SelectList(
            departments,
            "Id",
            "DepartmentName",
            departmentId);

        if (departmentId.HasValue)
        {
            employees = employees
                .Where(e => e.DepartmentId == departmentId.Value)
                .ToList();
        }

        int pageSize = 7;

        var data = employees
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;

        ViewBag.TotalPages =
            (int)Math.Ceiling(employees.Count / (double)pageSize);

        return View(data);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new EmployeeViewModel();

        var departments =
            await _departmentService.GetAllAsync();

        model.Departments =
            departments.Select(d =>
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.DepartmentName
            }).ToList();

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    EmployeeViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var departments =
                    await _departmentService.GetAllAsync();

                model.Departments =
                    departments.Select(d =>
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.DepartmentName
                    }).ToList();

                return View(model);
            }

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

            var deptList =
                await _departmentService.GetAllAsync();

            model.Departments =
                deptList.Select(d =>
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DepartmentName
                }).ToList();

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

            var departments =
                await _departmentService.GetAllAsync();

            employee.Departments =
                departments.Select(d =>
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DepartmentName
                }).ToList();

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
    public async Task<IActionResult> Edit(EmployeeViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var departments =
                    await _departmentService.GetAllAsync();

                model.Departments =
                    departments.Select(d =>
                    new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.DepartmentName
                    }).ToList();

                return View(model);
            }

            var error =
                await _service.UpdateAsync(model);

            if (error == null)
            {
                TempData["Success"] =
                    "Employee updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] =
                $"API Error: {error}";

            ModelState.AddModelError(
                string.Empty,
                error);

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["Error"] =
                ex.ToString();

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

            var departments =
                await _departmentService.GetAllAsync();

            foreach (var employee in employees)
            {
                employee.DepartmentName = departments
                    .FirstOrDefault(d => d.Id == employee.DepartmentId)
                    ?.DepartmentName;
            }

            ViewBag.Departments = new SelectList(
                departments,
                "Id",
                "DepartmentName");

            return View("Index", employees);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;

            return View("Index",
                new List<EmployeeViewModel>());
        }
    }


}

