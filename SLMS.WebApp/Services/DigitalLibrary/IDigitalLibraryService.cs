using SLMS.WebApp.Models.DigitalLibrary;

namespace SLMS.WebApp.Services.DigitalLibrary;

public interface IDigitalLibraryService
{
    Task<List<DigitalContentViewModel>> GetContentsAsync();

    Task SubmitRequestAsync(
    DigitalContentRequestViewModel model);

    Task<List<PolicyViewModel>> GetPoliciesAsync();

    Task<List<DownloadHistoryViewModel>>
    GetDownloadHistoryAsync();
}