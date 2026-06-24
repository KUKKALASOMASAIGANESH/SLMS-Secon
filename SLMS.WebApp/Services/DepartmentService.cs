using System.Net.Http.Json;
using SLMS.WebApp.Models;
using SLMS.WebApp.ViewModels;

namespace SLMS.WebApp.Services;

public class DepartmentService
{
    private readonly HttpClient _httpClient;

    public DepartmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DepartmentViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("api/Department");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Department API Error: " + error);

            return new List<DepartmentViewModel>();
        }

        var apiResponse = await response.Content
            .ReadFromJsonAsync<ApiResponseOfT<List<DepartmentViewModel>>>();

        return apiResponse?.Data ?? new List<DepartmentViewModel>();
    }

    public async Task<DepartmentViewModel?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/Department/{id}");

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content
            .ReadFromJsonAsync<ApiResponseOfT<DepartmentViewModel>>();

        return apiResponse?.Data;
    }

    public async Task<string?> CreateAsync(DepartmentViewModel department)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/Department",
            department);

        if (response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadAsStringAsync();
    }
}