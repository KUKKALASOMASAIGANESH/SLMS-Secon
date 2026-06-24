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

    Task CreateContentAsync(
    AdminDigitalContentViewModel model);

    Task UpdateContentAsync(
        AdminDigitalContentViewModel model);

    Task DeleteContentAsync(int id);

    Task<DigitalContentViewModel?> GetContentByIdAsync(int id);

    Task<List<AdminRequestViewModel>> GetRequestsAsync();

    Task ApproveRequestAsync(int id);

    Task RejectRequestAsync(int id);

    Task<List<PolicyViewModel>>
    GetPoliciesForAdminAsync();

    Task CreatePolicyAsync(
        AdminPolicyViewModel model);

    Task UpdatePolicyAsync(
        AdminPolicyViewModel model);

    Task DeletePolicyAsync(int id);

    Task<AdminPolicyViewModel?>
        GetPolicyByIdAsync(int id);

    Task<bool> CanAccessContentAsync(
    int contentId,
    int employeeId);

    Task RecordDownloadAsync(int contentId);

    Task AddDownloadHistoryAsync(
     int employeeId,
     int digitalContentId);

    Task<List<RequestStatusViewModel>>
    GetMyRequestsAsync(int employeeId);
        
}