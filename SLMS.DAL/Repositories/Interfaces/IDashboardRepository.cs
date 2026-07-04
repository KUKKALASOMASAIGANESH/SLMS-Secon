using SLMS.Shared.DTOs;
using SLMS.Shared.DTOs.Dashboard;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardAsync();
    Task<DashboardAnalyticsDto> GetAnalyticsAsync();
}