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
        var result =
            await _httpClient.GetFromJsonAsync<List<BookIssueViewModel>>
            ("https://localhost:7277/api/BookIssue");

        return result ?? new List<BookIssueViewModel>();
    }

    public async Task<BookIssueViewModel?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<BookIssueViewModel>
        ($"https://localhost:7277/api/BookIssue/{id}");
    }

    public async Task CreateAsync(BookIssueViewModel model)
    {
        var response =
            await _httpClient.PostAsJsonAsync(
                "https://localhost:7277/api/BookIssue",
                model);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(BookIssueViewModel model)
    {
        var response =
            await _httpClient.PutAsJsonAsync(
                $"https://localhost:7277/api/BookIssue/{model.Id}",
                model);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response =
            await _httpClient.DeleteAsync(
                $"https://localhost:7277/api/BookIssue/{id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<EmployeeViewModel>>
        GetEmployeesAsync()
    {
        return await _httpClient.GetFromJsonAsync
            <List<EmployeeViewModel>>
            ("https://localhost:7277/api/Employee")
            ?? new List<EmployeeViewModel>();
    }

    public async Task<List<LibraryResourceViewModel>>
        GetLibraryResourcesAsync()
    {
        return await _httpClient.GetFromJsonAsync
            <List<LibraryResourceViewModel>>
            ("https://localhost:7277/api/LibraryResource")
            ?? new List<LibraryResourceViewModel>();
    }
}