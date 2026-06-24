using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.Shared.DTOs.Reports;
using System;

public class ReportService
{
    private readonly SLMSDbContext _context;

    public ReportService(SLMSDbContext context)
    {
        _context = context;
    }

    //// 1. Inventory Report
    ////public async Task<List<InventoryReportDto>> GetInventoryReport()
    ////{
    //    //return await _context.InventoryItems
    //        //.GroupBy(x => new
    //        //{
    //            //x.Resource.Title,
    //            CategoryName = x.Resource.Category.Name
    //        })
    //        .Select(g => new InventoryReportDto
    //        {
    //            Title = g.Key.Title,
    //            Category = g.Key.CategoryName,
    //            TotalCount = g.Count()
    //        })
    //        .ToListAsync();
    //}

    // 2. Issue Report
    public async Task<List<IssueReportDto>> GetIssueReport()
    {
        return await _context.BookIssues
            .Where(x => x.Status == "Issued")
            .Select(x => new IssueReportDto
            {
                BookTitle = x.LibraryResource.Title,
                EmployeeName = x.Employee.FullName,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate
            })
            .ToListAsync();
    }

    // 3. Overdue Report
    public async Task<List<OverdueReportDto>> GetOverdueReport()
    {
        return await _context.BookIssues
            .Where(x => x.Status == "Issued" && x.DueDate < DateTime.UtcNow)
            .Select(x => new OverdueReportDto
            {
                BookTitle = x.LibraryResource.Title,
                EmployeeName = x.Employee.FullName,
                DaysOverdue = (DateTime.UtcNow - x.DueDate).Days
            })
            .ToListAsync();
    }

    // 4. Fine Report (calculated)
    public async Task<List<FineReportDto>> GetFineReport()
    {
        return await _context.BookIssues
            .Where(x => x.Status == "Issued" && x.DueDate < DateTime.UtcNow)
            .Select(x => new FineReportDto
            {
                EmployeeName = x.Employee.FullName,
                FineAmount = (DateTime.UtcNow - x.DueDate).Days * 10
            })
            .ToListAsync();
    }
}