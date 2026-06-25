using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class BookIssueService
{
    private readonly HttpClient _httpClient;

    public BookIssueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BookIssueViewModel>> GetAllAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<BookIssueViewModel>>("api/BookIssue")
            ?? new List<BookIssueViewModel>();
    }

    public async Task<BookIssueViewModel?> GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<BookIssueViewModel>($"api/BookIssue/{id}");
    }

    public async Task CreateAsync(BookIssueViewModel model)
    {
        var response = await _httpClient
            .PostAsJsonAsync("api/BookIssue", model);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(BookIssueViewModel model)
    {
        var response = await _httpClient
            .PutAsJsonAsync($"api/BookIssue/{model.Id}", model);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient
            .DeleteAsync($"api/BookIssue/{id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<EmployeeViewModel>> GetEmployeesAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<EmployeeViewModel>>("api/Employee")
            ?? new List<EmployeeViewModel>();
    }

    public async Task<List<LibraryResourceViewModel>> GetLibraryResourcesAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<LibraryResourceViewModel>>("api/LibraryResource")
            ?? new List<LibraryResourceViewModel>();
    }
}