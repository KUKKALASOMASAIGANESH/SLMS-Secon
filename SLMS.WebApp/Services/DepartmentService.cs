using System.Net.Http.Json;

using SLMS.WebApp.Models;
using SLMS.WebApp.Models.Common;

namespace SLMS.WebApp.Services;

public class DepartmentService
{
    private readonly HttpClient _httpClient;

    public DepartmentService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DepartmentViewModel>> GetAllAsync()
    {
        var response =
            await _httpClient.GetFromJsonAsync<
                ApiResponse<List<DepartmentViewModel>>>(
                    "api/Department");

        return response?.Data
            ?? new List<DepartmentViewModel>();
    }

    public async Task<DepartmentViewModel?> GetByIdAsync(int id)
    {
        var response =
            await _httpClient.GetFromJsonAsync<
                ApiResponse<DepartmentViewModel>>(
                    $"api/Department/{id}");

        return response?.Data;
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

        return await response
            .Content
            .ReadAsStringAsync();
    }

    public async Task<string?> UpdateAsync(
        DepartmentViewModel department)
    {
        var response =
            await _httpClient.PutAsJsonAsync(
                $"api/Department/{department.Id}",
                department);

        if (response.IsSuccessStatusCode)
            return null;

        return await response
            .Content
            .ReadAsStringAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response =
            await _httpClient.DeleteAsync(
                $"api/Department/{id}");

        return response.IsSuccessStatusCode;
    }
    public async Task<List<DepartmentViewModel>>
SearchAsync(string keyword)
    {
        var response =
            await _httpClient.GetFromJsonAsync<
            ApiResponse<List<DepartmentViewModel>>>
            ($"api/Department/search/{keyword}");

        return response?.Data ??
            new List<DepartmentViewModel>();
    }
}