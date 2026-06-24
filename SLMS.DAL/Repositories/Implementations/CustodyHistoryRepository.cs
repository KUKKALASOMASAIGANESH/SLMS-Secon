using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Custody;

namespace SLMS.DAL.Repositories.Implementations;

public class CustodyHistoryRepository
    : Repository<CustodyHistory>, ICustodyHistoryRepository
{
    private readonly SLMSDbContext _context;

    public CustodyHistoryRepository(
        SLMSDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustodyHistory>>
        GetByInventoryItemAsync(int inventoryItemId)
    {
        return await _dbSet
            .Include(x => x.FromDepartment)
            .Include(x => x.ToDepartment)
            .Include(x => x.InventoryItem)
            .Where(x => x.InventoryItemId == inventoryItemId)
            .OrderBy(x => x.TransferDate)
            .ToListAsync();
    }

    public async Task<CustodyHistory?>
        GetCurrentCustodianAsync(int inventoryItemId)
    {
        return await _dbSet
            .Include(x => x.FromDepartment)
            .Include(x => x.ToDepartment)
            .Include(x => x.InventoryItem)
            .Where(x => x.InventoryItemId == inventoryItemId)
            .OrderByDescending(x => x.TransferDate)
            .ThenByDescending(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CustodyHistory>>
        GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(x => x.FromDepartment)
            .Include(x => x.ToDepartment)
            .Include(x => x.InventoryItem)
            .ToListAsync();
    }

    public async Task<IEnumerable<CustodyHistoryReportDto>>
     GetCustodyReportAsync()
    {
        var issuedRecords =
            await _context.BookIssues
                .Include(x => x.Employee)
                    .ThenInclude(x => x.Department)
                .Include(x => x.LibraryResource)
                .Select(x =>
                    new CustodyHistoryReportDto
                    {
                        ResourceTitle = x.LibraryResource.Title,
                        EmployeeName = x.Employee.FullName,
                        DepartmentName = x.Employee.Department!.DepartmentName,

                        IssueDate = x.IssueDate,
                        ReturnDate = null,

                        Action = "Issued",
                        Status = "Active"
                    })
                .ToListAsync();

        var returnedRecords =
            await _context.BookReturns
                .Include(x => x.BookIssue)
                    .ThenInclude(x => x.Employee)
                        .ThenInclude(x => x.Department)
                .Include(x => x.BookIssue)
                    .ThenInclude(x => x.LibraryResource)
                .Select(x =>
                    new CustodyHistoryReportDto
                    {
                        ResourceTitle =
                            x.BookIssue.LibraryResource.Title,

                        EmployeeName =
                            x.BookIssue.Employee.FullName,

                        DepartmentName =
                            x.BookIssue.Employee.Department!.DepartmentName,

                        IssueDate =
                            x.BookIssue.IssueDate,

                        ReturnDate =
                            x.ReturnDate,

                        Action = "Returned",
                        Status = "Closed"
                    })
                .ToListAsync();

        return issuedRecords
            .Concat(returnedRecords)
            .OrderByDescending(x => x.IssueDate)
            .ToList();
    }
}