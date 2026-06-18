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
            employeeId = 1,
            approvalStatus = "Pending",
            requestDate = DateTime.UtcNow
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
}