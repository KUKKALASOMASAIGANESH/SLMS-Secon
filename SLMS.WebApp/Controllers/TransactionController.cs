using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.Shared.DTOs.BookIssue;
using SLMS.Shared.DTOs.BookReturn;
using SLMS.Shared.DTOs.Employee;
using SLMS.Shared.DTOs.Request;
using SLMS.WebApp.Services.Transaction.Interfaces;
using System.Net.Http.Json;
using System.Security.Claims;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin,Librarian,User")]
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
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> Dashboard()
    {
        var model = await _service.GetDashboardAsync();
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> ReturnBook()
    {
        var employees =
            await _httpClient.GetFromJsonAsync<List<EmployeeResponseDto>>
            ("api/Employee");

        ViewBag.Employees =
            employees ?? new List<EmployeeResponseDto>();

        return View(new BookReturnCreateDto
        {
            ReturnDate = DateTime.Today
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> GetIssuedBooks(int employeeId)
    {
        var books = await _httpClient
            .GetFromJsonAsync<List<BookIssueResponseDto>>(
                $"api/BookIssue/employee/{employeeId}");

        return Json(books ?? new List<BookIssueResponseDto>());
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> ReturnBook(BookReturnCreateDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return RedirectToAction("Login", "Auth");

        dto.ReturnedByUserId = int.Parse(userIdClaim);

       

        var response = await _httpClient
            .PostAsJsonAsync("api/BookReturn", dto);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Book Returned Successfully";
            return RedirectToAction(nameof(ReturnBook));
        }

        var error =
    await response.Content.ReadAsStringAsync();

        TempData["Error"] = error;

        // Reload employee list
        var employees =
            await _httpClient.GetFromJsonAsync<List<EmployeeResponseDto>>
            ("api/Employee");

        ViewBag.Employees =
            employees ?? new List<EmployeeResponseDto>();

        return View(dto);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> Requests()
    {
        var data = await _httpClient
            .GetFromJsonAsync<List<RequestResponseDto>>("api/Request");

        return View(data ?? new List<RequestResponseDto>());
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> ApproveRequest(int id)
    {
        await _httpClient.PutAsync(
            $"api/Request/{id}/approve",
            null);

        return RedirectToAction(nameof(Requests));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<IActionResult> RejectRequest(int id)
    {
        await _httpClient.PutAsync(
            $"api/Request/{id}/reject",
            null);

        return RedirectToAction(nameof(Requests));
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Librarian,User")]
    public async Task<IActionResult> OverdueBooks()
    {
        var data = await _httpClient
            .GetFromJsonAsync<List<BookIssueResponseDto>>(
                "api/BookIssue/overdue");

        return View(data ?? new List<BookIssueResponseDto>());
    }
}