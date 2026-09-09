using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.Shared.DTOs;
using SLMS.Shared.DTOs.Dashboard;

namespace SLMS.BLL.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(
        IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        return await _dashboardRepository
            .GetDashboardAsync();
    }
   
    public async Task<DashboardAnalyticsDto> GetAnalyticsAsync()
    {
        return await _dashboardRepository
            .GetAnalyticsAsync();
    }
}