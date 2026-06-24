using System.Net.Http.Json;

using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class LibraryResourceService
{
    private readonly HttpClient _httpClient;

    public LibraryResourceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LibraryResourceViewModel>> GetAllAsync()
    {
        try
        {
            var result =
                await _httpClient.GetFromJsonAsync<List<LibraryResourceViewModel>>
                ("https://localhost:7277/api/LibraryResource");

            return result ?? new List<LibraryResourceViewModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task<LibraryResourceViewModel?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient
                .GetFromJsonAsync<LibraryResourceViewModel>(
                    $"https://localhost:7277/api/LibraryResource/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task UpdateAsync(
    LibraryResourceViewModel model)
    {
        try
        {
            var response =
                await _httpClient.PutAsJsonAsync(
                    $"https://localhost:7277/api/LibraryResource/{model.Id}",
                    model);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task DeleteAsync(int id)
    {
        try
        {
            var response =
                await _httpClient.DeleteAsync(
                    $"https://localhost:7277/api/LibraryResource/{id}");

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task CreateAsync(
    LibraryResourceViewModel model)
    {
        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "https://localhost:7277/api/LibraryResource",
                    model);

            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                throw new Exception(error);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}