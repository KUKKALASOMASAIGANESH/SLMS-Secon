using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Models;
using System.Net.Http.Json;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin,Librarian")]
public class ReportsController : Controller
{
    private readonly HttpClient _httpClient;

    public ReportsController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("SLMSApi");
    }

    public async Task<IActionResult> Index()
    {
        var issues = await _httpClient
            .GetFromJsonAsync<List<IssueReport>>(
                "api/reports/issues");

        var overdue = await _httpClient
            .GetFromJsonAsync<List<OverdueReport>>(
                "api/reports/overdue");

        var fine = await _httpClient
            .GetFromJsonAsync<List<FineReport>>(
                "api/reports/fine");

        ViewBag.Issues = issues ?? new List<IssueReport>();
        ViewBag.Overdue = overdue ?? new List<OverdueReport>();
        ViewBag.Fine = fine ?? new List<FineReport>();

        return View();
    }
}