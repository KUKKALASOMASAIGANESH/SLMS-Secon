using System.Net.Http.Json;
using SLMS.WebApp.Models.Inventory;
using SLMS.WebApp.Services.Interfaces;

namespace SLMS.WebApp.Services;

public class ShelfService : IShelfService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "api/Shelf";

    public ShelfService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ShelfViewModel>> GetAllAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<List<ShelfViewModel>>(ApiUrl);

        return result ?? new List<ShelfViewModel>();
    }

    public async Task<ShelfViewModel?> GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<ShelfViewModel>($"{ApiUrl}/{id}");
    }

    public async Task CreateAsync(CreateShelfViewModel model)
    {
        var response =
            await _httpClient.PostAsJsonAsync(ApiUrl, model);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            if (error.Contains("Shelf name already exists"))
                throw new Exception("Shelf name already exists.");

            throw new Exception(error);
        }
    }

    public async Task UpdateAsync(int id, UpdateShelfViewModel model)
    {
        var response =
            await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", model);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            if (error.Contains("Shelf name already exists"))
                throw new Exception("Shelf name already exists.");

            if (error.Contains("Capacity cannot be less than current book count"))
                throw new Exception(error);

            throw new Exception(error);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var response =
            await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}