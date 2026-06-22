using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

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
    public async Task<IActionResult> Edit(
        EmployeeViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "ModelState Invalid";
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

            ModelState.AddModelError(
                string.Empty,
                error);

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
