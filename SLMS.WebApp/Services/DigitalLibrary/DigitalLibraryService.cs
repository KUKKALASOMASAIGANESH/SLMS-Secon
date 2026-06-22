using System.Net.Http.Json;
using SLMS.WebApp.Models.DigitalLibrary;

namespace SLMS.WebApp.Services.DigitalLibrary;

public class DigitalLibraryService
    : IDigitalLibraryService
{
    private readonly HttpClient _httpClient;

    public DigitalLibraryService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DigitalContentViewModel>>
        GetContentsAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                List<DigitalContentViewModel>>
            ("https://localhost:7277/api/DigitalContent");

        return result ?? new();
    }

    public async Task SubmitRequestAsync(
    DigitalContentRequestViewModel model)
    {
        var request = new
        {
            digitalContentId = model.DigitalContentId,
            reason = model.Reason
        };

        await _httpClient.PostAsJsonAsync(
            "https://localhost:7277/api/DigitalContentRequest",
            request);
    }

    public async Task<List<PolicyViewModel>>
    GetPoliciesAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                List<PolicyViewModel>>
            ("https://localhost:7277/api/Policy");

        return result ?? new();
    }

    public async Task<List<DownloadHistoryViewModel>>
     GetDownloadHistoryAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                List<DownloadHistoryApiResponse>>
            ("https://localhost:7277/api/DownloadHistory");

        if (result == null)
            return new();

        return result.Select(x =>
            new DownloadHistoryViewModel
            {
                ContentTitle =
                    x.DigitalContent?.Title ?? "Unknown",

                DownloadedOn = x.DownloadedOn
            })
            .ToList();
    }

    public async Task CreateContentAsync(
    AdminDigitalContentViewModel model)
    {
        await _httpClient.PostAsJsonAsync(
            "https://localhost:7277/api/DigitalContent",
            model);
    }

    public async Task UpdateContentAsync(
    AdminDigitalContentViewModel model)
    {
        await _httpClient.PutAsJsonAsync(
            $"https://localhost:7277/api/DigitalContent/{model.Id}",
            model);
    }

    public async Task DeleteContentAsync(int id)
    {
        await _httpClient.DeleteAsync(
            $"https://localhost:7277/api/DigitalContent/{id}");
    }

    public async Task<DigitalContentViewModel?> GetContentByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<DigitalContentViewModel>(
            $"https://localhost:7277/api/DigitalContent/{id}");
    }

    public async Task<List<AdminRequestViewModel>>
GetRequestsAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                List<AdminRequestViewModel>>
            ("https://localhost:7277/api/DigitalContentRequest");

        return result ?? new();
    }

    public async Task ApproveRequestAsync(int id)
    {
        await _httpClient.PutAsync(
            $"https://localhost:7277/api/DigitalContentRequest/approve/{id}",
            null);
    }

    public async Task RejectRequestAsync(int id)
    {
        await _httpClient.PutAsync(
            $"https://localhost:7277/api/DigitalContentRequest/reject/{id}",
            null);
    }

    public async Task<List<PolicyViewModel>>
    GetPoliciesForAdminAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<PolicyViewModel>>
            ("https://localhost:7277/api/Policy")
            ?? new();
    }

    public async Task CreatePolicyAsync(
    AdminPolicyViewModel model)
    {
        var dto = new
        {
            policyTitle =
                model.PolicyTitle,

            policyContent =
                model.PolicyContent
        };

        await _httpClient.PostAsJsonAsync(
            "https://localhost:7277/api/Policy",
            dto);
    }

    public async Task UpdatePolicyAsync(
    AdminPolicyViewModel model)
    {
        var dto = new
        {
            policyTitle =
                model.PolicyTitle,

            policyContent =
                model.PolicyContent
        };

        await _httpClient.PutAsJsonAsync(
            $"https://localhost:7277/api/Policy/{model.Id}",
            dto);
    }

    public async Task DeletePolicyAsync(
    int id)
    {
        await _httpClient.DeleteAsync(
            $"https://localhost:7277/api/Policy/{id}");
    }

    public async Task<AdminPolicyViewModel?>
    GetPolicyByIdAsync(int id)
    {
        var policy =
            await _httpClient.GetFromJsonAsync
            <PolicyViewModel>(
                $"https://localhost:7277/api/Policy/{id}");

        if (policy == null)
            return null;

        return new AdminPolicyViewModel
        {
            Id = policy.Id,
            PolicyTitle = policy.Title,
            PolicyContent = policy.Description
        };
    }
}