using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class CategoryService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl =
        "https://localhost:7277/api/Category";

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoryViewModel>> GetAllAsync()
    {
        try
        {
            var result =
                await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>
                ("https://localhost:7277/api/Category");

            return result ?? new List<CategoryViewModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine("Get Categories Completed");
        }
    }

    public async Task CreateAsync(CategoryViewModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7277/api/Category",
                model);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine("Create Category Completed");
        }
    }

    public async Task UpdateAsync(CategoryViewModel model)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7277/api/Category/{model.Id}",
                model);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine("Update Category Completed");
        }
    }
    public async Task<CategoryViewModel?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CategoryViewModel>(
                $"https://localhost:7277/api/Category/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task DeleteAsync(int id)
    {
        var response =
            await _httpClient.DeleteAsync(
                $"{ApiUrl}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var error =
                await response.Content
                    .ReadAsStringAsync();

            throw new Exception(error);
        }
    }

}