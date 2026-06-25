using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class DashboardService
{
    private readonly HttpClient _httpClient;

    public DashboardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<DashboardViewModel>(
                "api/Dashboard");

        return result ?? new DashboardViewModel();
    }
}