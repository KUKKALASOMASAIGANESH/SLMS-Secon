using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
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
}