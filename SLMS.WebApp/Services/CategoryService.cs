using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class CategoryService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "api/Category";

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoryViewModel>> GetAllAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>(ApiUrl);

        return result ?? new List<CategoryViewModel>();
    }

    public async Task<CategoryViewModel?> GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<CategoryViewModel>($"{ApiUrl}/{id}");
    }

    public async Task CreateAsync(CategoryViewModel model)
    {
        var response =
            await _httpClient.PostAsJsonAsync(ApiUrl, model);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(CategoryViewModel model)
    {
        var response =
            await _httpClient.PutAsJsonAsync($"{ApiUrl}/{model.Id}", model);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response =
            await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var error =
                await response.Content.ReadAsStringAsync();

            throw new Exception(error);
        }
    }
}