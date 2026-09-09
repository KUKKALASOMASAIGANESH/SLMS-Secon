using SLMS.Shared.DTOs;
using SLMS.Shared.DTOs.Dashboard;

namespace SLMS.BLL.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
    Task<DashboardAnalyticsDto> GetAnalyticsAsync();

}