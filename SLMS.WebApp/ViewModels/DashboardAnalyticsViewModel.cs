namespace SLMS.WebApp.Models.Dashboard;

public class DashboardAnalyticsViewModel
{
    public string MostBorrowedBook { get; set; } = string.Empty;

    public string LeastBorrowedBook { get; set; } = string.Empty;

    public string MostActiveEmployee { get; set; } = string.Empty;

    public string MostActiveDepartment { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;
}