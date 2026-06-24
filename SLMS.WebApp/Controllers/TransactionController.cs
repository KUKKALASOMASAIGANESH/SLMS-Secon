using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
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
    // Return Book
    // =====================================

    [HttpGet]
    public async Task<IActionResult>
ReturnBook()
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var employees =
            await client.GetFromJsonAsync<
                List<EmployeeViewModel>>
                ("api/Employee");

        ViewBag.Employees =
            employees;

        return View(
            new BookReturnCreateDto
            {
                ReturnDate =
                    DateTime.Today
            });
    }
    [HttpGet]
    public async Task<IActionResult>
GetIssuedBooks(int employeeId)
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");

        var books =
            await client.GetFromJsonAsync<
                List<BookIssueResponseDto>>
                ($"api/BookIssue/employee/{employeeId}");

        return Json(books);
    }

    [HttpPost]
    public async Task<IActionResult>
        ReturnBook(BookReturnCreateDto dto)
    {
        var client = new HttpClient();

        client.BaseAddress =
            new Uri("http://localhost:5062/");
        dto.ReturnedByUserId = 1;

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