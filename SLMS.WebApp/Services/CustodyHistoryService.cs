using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class CustodyHistoryService
{
    private readonly HttpClient _httpClient;

    public CustodyHistoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CustodyHistoryViewModel>>
        GetAllAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<CustodyHistoryViewModel>>
            ("api/CustodyHistory")
            ?? new List<CustodyHistoryViewModel>();
    }

    public async Task<CustodyHistoryViewModel?> GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<CustodyHistoryViewModel>(
                $"api/CustodyHistory/{id}");
    }

    public async Task<bool> CreateAsync(
     CustodyHistoryViewModel custody)
    {
        var response = await _httpClient
            .PostAsJsonAsync(
                "api/CustodyHistory",
                custody);

        return response.IsSuccessStatusCode;
    }
    public async Task<List<CustodyHistoryViewModel>>
    GetByInventoryItemAsync(int inventoryItemId)
    {
        return await _httpClient
            .GetFromJsonAsync<List<CustodyHistoryViewModel>>(
                $"api/CustodyHistory/inventory/{inventoryItemId}")
            ?? new List<CustodyHistoryViewModel>();
    }

    public async Task<CustodyHistoryViewModel?>
        GetCurrentCustodianAsync(int inventoryItemId)
    {
        return await _httpClient
            .GetFromJsonAsync<CustodyHistoryViewModel>(
                $"api/CustodyHistory/current/{inventoryItemId}");
    }
    public async Task<List<CustodyHistoryReportViewModel>>
    GetReportAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<CustodyHistoryReportViewModel>>
            ("api/CustodyHistory/report")
            ?? new List<CustodyHistoryReportViewModel>();
    }

}