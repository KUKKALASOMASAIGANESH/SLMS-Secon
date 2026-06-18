using System.Net.Http.Json;
using SLMS.WebApp.Models.Inventory;
using SLMS.WebApp.Services.Interfaces;
using SLMS.WebApp.Models.Common;
namespace SLMS.WebApp.Services;

public class InventoryService : IInventoryService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl =
     "https://localhost:7277/api/Inventory";

    public InventoryService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Retrieves all inventory items from the API
    // and extracts data from the response wrapper
    public async Task<List<InventoryViewModel>>
    GetAllAsync()
    {
        var response =
            await _httpClient
                .GetFromJsonAsync<
                    ApiResponse<List<InventoryViewModel>>>(
                        ApiUrl);

        return response?.Data ??
               new List<InventoryViewModel>();
    }

    // Retrieves a specific inventory item from the API
    // using its unique identifier
    
    public async Task<InventoryViewModel?>
        GetByIdAsync(int id)
    {
        var response =
            await _httpClient
                .GetFromJsonAsync<
                    ApiResponse<InventoryViewModel>>(
                        $"{ApiUrl}/{id}");

        return response?.Data;
    }

    // Sends a request to the API to create
    // a new inventory item
    public async Task CreateAsync(
        CreateInventoryViewModel model)
    {
        await _httpClient.PostAsJsonAsync(
            ApiUrl,
            model);
    }

    // Sends updated inventory information
    // to the API for persistence
    public async Task UpdateAsync(
        int id,
        UpdateInventoryViewModel model)
    {
        await _httpClient.PutAsJsonAsync(
            $"{ApiUrl}/{id}",
            model);
    }

    // Removes the specified inventory item
    // through the API
    // Deletes an inventory item
    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync(
            $"{ApiUrl}/{id}");
    }

    // Searches inventory items by accession number
    public async Task<List<InventoryViewModel>>
        SearchAsync(string searchTerm)
    {
        var result =
            await _httpClient
                .GetFromJsonAsync<
                    List<InventoryViewModel>>
                    ($"{ApiUrl}/search?AccessionNumber={searchTerm}");

        return result ??
               new List<InventoryViewModel>();
    }


    // Retrieves paged inventory records
    public async Task<InventoryPagedViewModel>
    GetPagedAsync(
        int page,
        int pageSize)
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                PagedResponse<InventoryViewModel>>
                ($"{ApiUrl}/paged?Page={page}&PageSize={pageSize}");

        if (result == null)
        {
            return new InventoryPagedViewModel();
        }

        return new InventoryPagedViewModel
        {
            Items = result.Items,
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
    }

    // Retrieves inventory summary report
    public async Task<InventorySummaryViewModel>
        GetSummaryAsync()
    {
        var result =
            await _httpClient
                .GetFromJsonAsync<
                    InventorySummaryViewModel>
                    ($"{ApiUrl}/report/summary");

        return result ??
               new InventorySummaryViewModel();
    }

    // Retrieves inventory grouped by resource
    public async Task<
        List<ResourceInventoryReportViewModel>>
        GetResourceReportAsync()
    {
        var result =
            await _httpClient
                .GetFromJsonAsync<
                    List<ResourceInventoryReportViewModel>>
                    ($"{ApiUrl}/report/resource");

        return result ??
               new List<
                   ResourceInventoryReportViewModel>();
    }

    // Retrieves inventory cost report
    public async Task<InventoryCostReportViewModel>
        GetCostReportAsync()
    {
        var result =
            await _httpClient
                .GetFromJsonAsync<
                    InventoryCostReportViewModel>
                    ($"{ApiUrl}/report/cost");

        return result ??
               new InventoryCostReportViewModel();
    }

    public async Task<
    InventoryAvailabilityReportViewModel>
    GetAvailabilityReportAsync()
    {
        var result =
            await _httpClient
                .GetFromJsonAsync<
                    InventoryAvailabilityReportViewModel>
                    ($"{ApiUrl}/report/availability");

        return result ??
               new InventoryAvailabilityReportViewModel();
    }

    public async Task<
    List<ShelfReportViewModel>>
    GetShelfReportAsync()
    {
        var result =
            await _httpClient
                .GetFromJsonAsync<
                    List<ShelfReportViewModel>>
                    ($"{ApiUrl}/report/shelf");

        return result ??
               new List<ShelfReportViewModel>();
    }

    // Retrieves inactive inventory items
    public async Task<List<InventoryViewModel>>
        GetInactiveAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                List<InventoryViewModel>>
                ($"{ApiUrl}/inactive");

        return result ??
               new List<InventoryViewModel>();
    }

    // Restores a soft-deleted inventory item
    public async Task RestoreAsync(int id)
    {
        await _httpClient.PutAsync(
            $"{ApiUrl}/restore/{id}",
            null);
    }
    public async Task<List<InventoryViewModel>>
    GetSortedAsync(
        string sortBy,
        bool descending)
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                List<InventoryViewModel>>
                ($"{ApiUrl}/sorted?SortBy={sortBy}&Descending={descending}");

        return result ??
               new List<InventoryViewModel>();
    }
}