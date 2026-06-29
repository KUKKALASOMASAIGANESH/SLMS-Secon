using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<EmployeeViewModel>>
        GetAllAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<EmployeeViewModel>>
            ("api/Employee")
            ?? new List<EmployeeViewModel>();
    }
    public async Task<EmployeeViewModel?> GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<EmployeeViewModel>(
                $"api/Employee/{id}");
    }

    public async Task<string?> UpdateAsync(
 EmployeeViewModel employee)
    {
        var response = await _httpClient
            .PutAsJsonAsync(
                $"api/Employee/{employee.Id}",
                employee);

        if (response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string?> CreateAsync(
    EmployeeViewModel employee)
    {
        var response = await _httpClient
            .PostAsJsonAsync(
                "api/Employee",
                employee);

        if (response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient
            .DeleteAsync($"api/Employee/{id}");

        return response.IsSuccessStatusCode;
    }
    public async Task<List<EmployeeViewModel>>
    SearchAsync(string name)
    {
        return await _httpClient
            .GetFromJsonAsync<List<EmployeeViewModel>>
            ($"api/Employee/search/{name}")
            ?? new List<EmployeeViewModel>();
    }

}