using System.Net.Http.Json;
using SLMS.WebApp.ViewModels;

namespace SLMS.WebApp.Services;

public class UserManagementService
{
    private readonly HttpClient _httpClient;

    public UserManagementService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserRoleViewModel>> GetAllAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<UserRoleViewModel>>("api/UserManagement")
            ?? new List<UserRoleViewModel>();
    }

    public async Task<bool> ChangeRoleAsync(int userId, int roleId)
    {
        var response = await _httpClient
            .PostAsync(
                $"api/UserManagement/change-role?userId={userId}&roleId={roleId}",
                null);

        return response.IsSuccessStatusCode;
    }
}