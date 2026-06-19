using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class DepartmentService
{
    private readonly HttpClient _httpClient;

    public DepartmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DepartmentViewModel>>
        GetAllAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<DepartmentViewModel>>
            ("api/Department")
            ?? new List<DepartmentViewModel>();
    }

    public async Task<DepartmentViewModel?>
        GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<DepartmentViewModel>
            ($"api/Department/{id}");
    }

    public async Task<string?> CreateAsync(
      DepartmentViewModel department)
    {
        var response =
            await _httpClient.PostAsJsonAsync(
                "api/Department",
                department);

        if (response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadAsStringAsync();
    }
}