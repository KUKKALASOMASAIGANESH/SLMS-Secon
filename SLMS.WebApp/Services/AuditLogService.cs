using System.Net.Http.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services;

public class AuditLogService
{
    private readonly HttpClient _httpClient;

    public AuditLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AuditLogViewModel>>
        GetAllAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<AuditLogViewModel>>
            ("api/AuditLog")
            ?? new List<AuditLogViewModel>();
    }

    public async Task<AuditLogViewModel?>
        GetByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<AuditLogViewModel>(
                $"api/AuditLog/{id}");
    }
}