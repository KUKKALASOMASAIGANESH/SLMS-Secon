using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc;

using SLMS.Shared.DTOs.BookIssue;
using SLMS.Shared.DTOs.BookReturn;
using SLMS.Shared.DTOs.Request;

using SLMS.WebApp.Services.Transaction.Interfaces;

namespace SLMS.WebApp.Controllers;

public class TransactionController : Controller
{
    private readonly
        ITransactionDashboardService _service;

    public TransactionController(
        ITransactionDashboardService service)
    {
        _service = service;
    }

    // =====================================
    // Dashboard
    // =====================================

    [HttpGet]
    public async Task<IActionResult>
        Dashboard()
    {
        var model =
            await _service.GetDashboardAsync();

        return View(model);
    }

    // =====================================
    // Issue Book
    // =====================================

    [HttpGet]
    public IActionResult IssueBook()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>
        IssueBook(BookIssueCreateDto dto)
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var response =
            await client.PostAsJsonAsync(
                "api/BookIssue",
                dto);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] =
                "Book Issued Successfully";

            return RedirectToAction(
                nameof(IssueBook));
        }

        TempData["Error"] =
            "Failed to Issue Book";

        return View(dto);
    }

    // =====================================
    // Return Book
    // =====================================

    [HttpGet]
    public IActionResult ReturnBook()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>
        ReturnBook(BookReturnCreateDto dto)
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var response =
            await client.PostAsJsonAsync(
                "api/BookReturn",
                dto);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] =
                "Book Returned Successfully";

            return RedirectToAction(
                nameof(ReturnBook));
        }

        TempData["Error"] =
            "Failed to Return Book";

        return View(dto);
    }

    // =====================================
    // Requests
    // =====================================

    [HttpGet]
    public async Task<IActionResult>
        Requests()
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var data =
            await client.GetFromJsonAsync<
                List<RequestResponseDto>>(
                "api/Request");

        return View(data);
    }

    // =====================================
    // Approve Request
    // =====================================

    [HttpPost]
    public async Task<IActionResult>
        ApproveRequest(int id)
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var response =
            await client.PutAsync(
                $"api/Request/{id}/approve",
                null);

        return RedirectToAction(
            nameof(Requests));
    }

    // =====================================
    // Reject Request
    // =====================================

    [HttpPost]
    public async Task<IActionResult>
        RejectRequest(int id)
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var response =
            await client.PutAsync(
                $"api/Request/{id}/reject",
                null);

        return RedirectToAction(
            nameof(Requests));
    }

    // =====================================
    // Overdue Books
    // =====================================

    [HttpGet]
    public async Task<IActionResult>
        OverdueBooks()
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var data =
            await client.GetFromJsonAsync<
                List<BookIssueResponseDto>>(
                "api/BookIssue/overdue");

        return View(data);
    }
}