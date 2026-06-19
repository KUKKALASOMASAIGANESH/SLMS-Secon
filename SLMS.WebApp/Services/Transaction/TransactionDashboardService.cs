using System.Net.Http.Json;

using SLMS.WebApp.Services.Transaction.Interfaces;
using SLMS.WebApp.ViewModels;

namespace SLMS.WebApp.Services.Transaction;

public class TransactionDashboardService
    : ITransactionDashboardService
{
    private readonly HttpClient _httpClient;

    public TransactionDashboardService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TransactionDashboardViewModel>
        GetDashboardAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<
                TransactionDashboardViewModel>(
                    "api/TransactionDashboard")
            ?? new TransactionDashboardViewModel();
    }
}