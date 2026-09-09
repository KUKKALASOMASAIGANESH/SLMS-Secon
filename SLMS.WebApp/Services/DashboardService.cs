using SLMS.Shared.DTOs;
using SLMS.Shared.DTOs.Dashboard;
using SLMS.WebApp.Models;
using System.Net.Http.Json;

namespace SLMS.WebApp.Services;

public class DashboardService
{
    private readonly HttpClient _httpClient;

    public DashboardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        // Statistics
        var dashboard =
            await _httpClient.GetFromJsonAsync<DashboardDto>(
                "api/Dashboard");

        // Analytics
        var analytics =
            await _httpClient.GetFromJsonAsync<DashboardAnalyticsDto>(
                "api/Dashboard/analytics");

        DashboardViewModel model = new();

        if (dashboard != null)
        {
            model.TotalDepartments = dashboard.TotalDepartments;
            model.TotalEmployees = dashboard.TotalEmployees;
            model.TotalCategories = dashboard.TotalCategories;
            model.TotalResources = dashboard.TotalResources;
            model.TotalShelves = dashboard.TotalShelves;
            model.TotalIssuedBooks = dashboard.TotalIssuedBooks;
            model.TotalReturnedBooks = dashboard.TotalReturnedBooks;
            model.TotalOverdueBooks = dashboard.TotalOverdueBooks;
            model.TotalDigitalContents = dashboard.TotalDigitalContents;
            model.TotalRequests = dashboard.TotalRequests;
            model.TotalUsers = dashboard.TotalUsers;
            model.TotalAuditLogs = dashboard.TotalAuditLogs;
        }

        if (analytics != null)
        {
            model.MostBorrowedBook = analytics.MostBorrowedBook;
            model.LeastBorrowedBook = analytics.LeastBorrowedBook;
            model.MostActiveEmployee = analytics.MostActiveEmployee;
            model.MostActiveDepartment = analytics.MostActiveDepartment;
            model.Recommendation = analytics.Recommendation;
        }

        return model;
    }
}