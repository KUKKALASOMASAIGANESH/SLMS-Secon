using Microsoft.EntityFrameworkCore;

using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class LibraryResourceRepository
    : Repository<LibraryResource>,
      ILibraryResourceRepository
{
    public LibraryResourceRepository(
        SLMSDbContext context)
        : base(context)
    {
    }

    public async Task<
        IEnumerable<LibraryResource>>
        SearchAsync(string keyword)
    {
        return await _context.LibraryResources
            .Where(x =>
                x.Title.Contains(keyword) ||
                x.Author.Contains(keyword) ||
                x.Publisher.Contains(keyword))
            .ToListAsync();
    }
}