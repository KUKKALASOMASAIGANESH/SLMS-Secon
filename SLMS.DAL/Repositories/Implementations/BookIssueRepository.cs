using Microsoft.EntityFrameworkCore;

using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;

using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class BookIssueRepository
    : Repository<BookIssue>,
      IBookIssueRepository
{
    private readonly SLMSDbContext _context;

    public BookIssueRepository(
        SLMSDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookIssue>>
        GetAllWithDetailsAsync()
    {
        return await _context.BookIssues
            .Include(x => x.Employee)
            .Include(x => x.LibraryResource)
                //.ThenInclude(x => x.ResourceType)
            .ToListAsync();
    }

    public async Task<BookIssue?>
        GetByIdWithDetailsAsync(int id)
    {
        return await _context.BookIssues
            .Include(x => x.Employee)
            .Include(x => x.LibraryResource)
                //.ThenInclude(x => x.Resource)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<BookIssue>>
        GetOverdueBooksAsync()
    {
        return await _context.BookIssues
            .Where(x =>
                x.Status == "Issued" &&
                x.DueDate < DateTime.UtcNow)
            .ToListAsync();
    }
}