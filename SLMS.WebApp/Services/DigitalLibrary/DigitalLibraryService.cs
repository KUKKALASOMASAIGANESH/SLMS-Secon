using System.Net.Http.Json;
using SLMS.WebApp.Models.DigitalLibrary;

namespace SLMS.WebApp.Services.DigitalLibrary;

public class DigitalLibraryService : IDigitalLibraryService
{
    private readonly HttpClient _httpClient;

    public DigitalLibraryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DigitalContentViewModel>> GetContentsAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<DigitalContentViewModel>>("api/DigitalContent")
            ?? new();
    }

    public async Task SubmitRequestAsync(DigitalContentRequestViewModel model)
    {
        var request = new
        {
            digitalContentId = model.DigitalContentId,
            reason = model.Reason
        };

        await _httpClient.PostAsJsonAsync("api/DigitalContentRequest", request);
    }

    public async Task<List<PolicyViewModel>> GetPoliciesAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<PolicyViewModel>>("api/Policy")
            ?? new();
    }

    public async Task<List<DownloadHistoryViewModel>> GetDownloadHistoryAsync()
    {
        var result = await _httpClient
            .GetFromJsonAsync<List<DownloadHistoryApiResponse>>("api/DownloadHistory");

        if (result == null)
            return new();

        return result.Select(x => new DownloadHistoryViewModel
        {
            ContentTitle = x.DigitalContent?.Title ?? "Unknown",
            DownloadedOn = x.DownloadedOn
        }).ToList();
    }

    public async Task CreateContentAsync(AdminDigitalContentViewModel model)
    {
        await _httpClient.PostAsJsonAsync("api/DigitalContent", model);
    }

    public async Task UpdateContentAsync(AdminDigitalContentViewModel model)
    {
        await _httpClient.PutAsJsonAsync(
            $"api/DigitalContent/{model.Id}",
            model);
    }

    public async Task DeleteContentAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/DigitalContent/{id}");
    }

    public async Task<DigitalContentViewModel?> GetContentByIdAsync(int id)
    {
        return await _httpClient
            .GetFromJsonAsync<DigitalContentViewModel>($"api/DigitalContent/{id}");
    }

    public async Task<List<AdminRequestViewModel>> GetRequestsAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<AdminRequestViewModel>>("api/DigitalContentRequest")
            ?? new();
    }

    public async Task ApproveRequestAsync(int id)
    {
        await _httpClient.PutAsync(
            $"api/DigitalContentRequest/approve/{id}",
            null);
    }

    public async Task RejectRequestAsync(int id)
    {
        await _httpClient.PutAsync(
            $"api/DigitalContentRequest/reject/{id}",
            null);
    }

    public async Task<List<PolicyViewModel>> GetPoliciesForAdminAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<PolicyViewModel>>("api/Policy")
            ?? new();
    }

    public async Task CreatePolicyAsync(AdminPolicyViewModel model)
    {
        var dto = new
        {
            policyTitle = model.PolicyTitle,
            policyContent = model.PolicyContent
        };

        await _httpClient.PostAsJsonAsync("api/Policy", dto);
    }

    public async Task UpdatePolicyAsync(AdminPolicyViewModel model)
    {
        var dto = new
        {
            policyTitle = model.PolicyTitle,
            policyContent = model.PolicyContent
        };

        await _httpClient.PutAsJsonAsync(
            $"api/Policy/{model.Id}",
            dto);
    }

    public async Task DeletePolicyAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/Policy/{id}");
    }

    public async Task<AdminPolicyViewModel?> GetPolicyByIdAsync(int id)
    {
        var policy = await _httpClient
            .GetFromJsonAsync<PolicyViewModel>($"api/Policy/{id}");

        if (policy == null)
            return null;

        return new AdminPolicyViewModel
        {
            Id = policy.Id,
            PolicyTitle = policy.Title,
            PolicyContent = policy.Description
        };
    }

    public async Task<bool> CanAccessContentAsync(int contentId, int employeeId)
    {
        var requests = await _httpClient
            .GetFromJsonAsync<List<AccessRequestViewModel>>("api/DigitalContentRequest");

        if (requests == null)
            return false;

        return requests.Any(x =>
            x.EmployeeId == employeeId &&
            x.DigitalContentId == contentId &&
            x.ApprovalStatus == "Approved");
    }

    public async Task RecordDownloadAsync(int contentId)
    {
        await _httpClient.PostAsync(
            $"api/DigitalContent/download/{contentId}",
            null);
    }

    public async Task AddDownloadHistoryAsync(int employeeId, int digitalContentId)
    {
        var dto = new
        {
            employeeId,
            digitalContentId
        };

        await _httpClient.PostAsJsonAsync("api/DownloadHistory", dto);
    }

    public async Task<List<RequestStatusViewModel>> GetMyRequestsAsync(int employeeId)
    {
        return await _httpClient
            .GetFromJsonAsync<List<RequestStatusViewModel>>(
                $"api/DigitalContentRequest/employee/{employeeId}")
            ?? new();
    }
}