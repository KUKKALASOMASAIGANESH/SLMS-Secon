using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.Shared.DTOs.BookIssue;
using SLMS.Shared.DTOs.BookReturn;
using SLMS.Shared.DTOs.Request;
using SLMS.WebApp.Models;
using SLMS.WebApp.Services.Transaction.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin,Librarian")]
public class TransactionController : Controller
{
    private readonly ITransactionDashboardService _service;
    private readonly HttpClient _httpClient;



    public TransactionController(
        ITransactionDashboardService service,
        IHttpClientFactory httpClientFactory)
    {
        _service = service;
        _httpClient = httpClientFactory.CreateClient("SLMSApi");
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var model = await _service.GetDashboardAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ReturnBook()
    {
        var employees =
            await _httpClient.GetFromJsonAsync<List<EmployeeViewModel>>(
                "api/Employee");

        ViewBag.Employees = employees;

        return View(new BookReturnCreateDto
        {
            ReturnDate = DateTime.Today
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetIssuedBooks(int employeeId)
    {
        var books =
            await _httpClient.GetFromJsonAsync<List<BookIssueResponseDto>>(
                $"api/BookIssue/employee/{employeeId}");

        return Json(books);
    }

    [HttpPost]
    public async Task<IActionResult> ReturnBook(BookReturnCreateDto dto)
    {
        dto.ReturnedByUserId = 1;

        var response =
            await _httpClient.PostAsJsonAsync(
                "api/BookReturn",
                dto);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Book Returned Successfully";
            return RedirectToAction(nameof(ReturnBook));
        }

        TempData["Error"] = "Failed to Return Book";
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Requests()
    {
        var data =
            await _httpClient.GetFromJsonAsync<List<RequestResponseDto>>(
                "api/Request");

        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveRequest(int id)
    {
        await _httpClient.PutAsync(
            $"api/Request/{id}/approve",
            null);

        return RedirectToAction(nameof(Requests));
    }

    [HttpPost]
    public async Task<IActionResult> RejectRequest(int id)
    {
        await _httpClient.PutAsync(
            $"api/Request/{id}/reject",
            null);

        return RedirectToAction(nameof(Requests));
    }

    [HttpGet]
    public async Task<IActionResult> OverdueBooks()
    {
        var data =
            await _httpClient.GetFromJsonAsync<List<BookIssueResponseDto>>(
                "api/BookIssue/overdue");

        return View(data);
    }
}