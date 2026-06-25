using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class LibraryResourceService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "api/LibraryResource";

    public LibraryResourceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LibraryResourceViewModel>> GetAllAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<List<LibraryResourceViewModel>>
            (ApiUrl);

        return result ?? new List<LibraryResourceViewModel>();
    }

    public async Task<LibraryResourceViewModel?> GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<LibraryResourceViewModel>(
                $"{ApiUrl}/{id}");
    }

    public async Task CreateAsync(
        LibraryResourceViewModel model)
    {
        var response =
            await _httpClient.PostAsJsonAsync(
                ApiUrl,
                model);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(
        LibraryResourceViewModel model)
    {
        var response =
            await _httpClient.PutAsJsonAsync(
                $"{ApiUrl}/{model.Id}",
                model);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response =
            await _httpClient.DeleteAsync(
                $"{ApiUrl}/{id}");

        response.EnsureSuccessStatusCode();
    }
}