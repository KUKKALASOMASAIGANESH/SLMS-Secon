using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.Shared.DTOs;
using SLMS.Shared.DTOs.Dashboard;

namespace SLMS.DAL.Repositories.Implementations;

public class DashboardRepository : IDashboardRepository
{
    private readonly SLMSDbContext _context;

    public DashboardRepository(SLMSDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        DashboardDto dashboard = new();

        dashboard.TotalDepartments =
            await _context.Departments.CountAsync();

        dashboard.TotalEmployees =
            await _context.Employees.CountAsync();

        dashboard.TotalCategories =
            await _context.Categories.CountAsync();

        dashboard.TotalResources =
            await _context.LibraryResources.CountAsync();

        dashboard.TotalShelves =
            await _context.Shelves.CountAsync();

        dashboard.TotalIssuedBooks =
            await _context.BookIssues.CountAsync();

        dashboard.TotalReturnedBooks =
            await _context.BookReturns.CountAsync();

        dashboard.TotalDigitalContents =
            await _context.DigitalContents.CountAsync();

        dashboard.TotalRequests =
            await _context.DigitalContentRequests.CountAsync();

        dashboard.TotalUsers =
            await _context.Users.CountAsync();

        dashboard.TotalAuditLogs =
            await _context.AuditLogs.CountAsync();

        dashboard.TotalOverdueBooks =
            await _context.BookIssues
                .Where(x => x.DueDate < DateTime.UtcNow &&
                            x.Status == "Issued")
                .CountAsync();

        return dashboard;
    }
    public async Task<DashboardAnalyticsDto> GetAnalyticsAsync()
    {
        DashboardAnalyticsDto analytics = new();

        // Most Borrowed Book
        analytics.MostBorrowedBook =
            await _context.BookIssues
                .GroupBy(x => x.LibraryResource.Title)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefaultAsync()
                ?? "No Data";

        // Least Borrowed Book
        analytics.LeastBorrowedBook =
            await (
                from resource in _context.LibraryResources
                join issue in _context.BookIssues
                    on resource.Id equals issue.LibraryResourceId
                    into issues
                orderby issues.Count()
                select resource.Title
            ).FirstOrDefaultAsync()
            ?? "No Data";

        // Most Active Employee
        analytics.MostActiveEmployee =
            await _context.BookIssues
                .GroupBy(x => x.Employee.FullName)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefaultAsync()
                ?? "No Data";

        // Most Active Department
        analytics.MostActiveDepartment =
            await _context.BookIssues
                .GroupBy(x => x.Employee.Department.DepartmentName)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefaultAsync()
                ?? "No Data";

        analytics.Recommendation =
            $"Purchase more books for {analytics.MostActiveDepartment} Department.";

        return analytics;
    }

}