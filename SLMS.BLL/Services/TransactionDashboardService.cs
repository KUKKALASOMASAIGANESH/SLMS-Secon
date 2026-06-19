using Microsoft.EntityFrameworkCore;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Data;
using SLMS.Shared.DTOs.Transaction;

namespace SLMS.BLL.Services;

public class TransactionDashboardService
    : ITransactionDashboardService
{
    private readonly SLMSDbContext _context;

    public TransactionDashboardService(
        SLMSDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionDashboardDto>
        GetDashboardAsync()
    {
        return new TransactionDashboardDto
        {
            TotalIssuedBooks =
                await _context.BookIssues.CountAsync(),

            TotalReturnedBooks =
                await _context.BookReturns.CountAsync(),

            OverdueBooks =
                await _context.BookIssues
                    .CountAsync(x =>
                        x.Status == "Issued" &&
                        x.DueDate < DateTime.UtcNow),

            TotalFineCollected =
                await _context.BookReturns
                    .SumAsync(x => x.FineAmount)
        };
    }
}