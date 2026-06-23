using System.Net.Http.Json;

using SLMS.WebApp.Models.Inventory;
using SLMS.WebApp.Services.Interfaces;

namespace SLMS.WebApp.Services;

public class ShelfService : IShelfService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl =
        "https://localhost:7277/api/Shelf";

    public ShelfService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ShelfViewModel>>
        GetAllAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                List<ShelfViewModel>>(ApiUrl);

        return result ??
               new List<ShelfViewModel>();
    }

    public async Task<ShelfViewModel?>
        GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<ShelfViewModel>(
                $"{ApiUrl}/{id}");
    }

    public async Task CreateAsync(
        CreateShelfViewModel model)
    {
        await _httpClient.PostAsJsonAsync(
            ApiUrl,
            model);
    }

    public async Task UpdateAsync(
        int id,
        UpdateShelfViewModel model)
    {
        await _httpClient.PutAsJsonAsync(
            $"{ApiUrl}/{id}",
            model);
    }

    public async Task DeleteAsync(int id)
    {
        var response =
            await _httpClient.DeleteAsync(
                $"{ApiUrl}/{id}");

        response.EnsureSuccessStatusCode();
    }
}