using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services;
using System.Linq;

namespace SLMS.WebApp.Controllers;


[Authorize(Roles = "Admin,Librarian")]
public class BookIssueController : Controller
{
    private readonly BookIssueService _service;

    public BookIssueController(
        BookIssueService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var issues =
            await _service.GetAllAsync();

        Console.WriteLine($"BOOK ISSUE COUNT = {issues.Count}");

        foreach (var item in issues)
        {
            Console.WriteLine(
                $"ID={item.Id} | Book={item.BookTitle} | Employee={item.EmployeeName}");
        }

        return View(issues);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new BookIssueViewModel();

        var employees =
            await _service.GetEmployeesAsync();

        model.EmployeeList =
            employees.Select(e =>
                new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.EmployeeNumber} - {e.FullName}"
                }).ToList();

        var books =
            await _service.GetLibraryResourcesAsync();

        model.BookList =
            books.Select(b =>
                new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Title
                }).ToList();

        model.IssueDate = DateTime.Today;
        model.Status = "Issued";

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        BookIssueViewModel model)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            await _service.CreateAsync(model);

            TempData["SuccessMessage"] =
                "Book Issued Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            TempData["ErrorMessage"] =
                ex.Message;

            return View(model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var issue =
            await _service.GetByIdAsync(id);

        if (issue == null)
            return NotFound();

        return View(issue);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        BookIssueViewModel model)
    {
        try
        {
            await _service.UpdateAsync(model);

            TempData["SuccessMessage"] =
                "Book Issue Updated Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            TempData["ErrorMessage"] =
                ex.Message;

            return View(model);
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var issue =
            await _service.GetByIdAsync(id);

        if (issue == null)
            return NotFound();

        return View(issue);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            TempData["SuccessMessage"] =
                "Book Issue Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            TempData["ErrorMessage"] =
                ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }
}