using SLMS.Shared.DTOs.Dashboard;

namespace SLMS.BLL.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
}