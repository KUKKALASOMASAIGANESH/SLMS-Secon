using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class CategoryRepository
    : Repository<Category>,
      ICategoryRepository
{
    public CategoryRepository(
        SLMSDbContext context)
        : base(context)
    {

    }

    public async Task<bool>
HasResourcesAsync(int categoryId)
    {
        return await _context.LibraryResources
            .AnyAsync(x =>
                x.CategoryId == categoryId);
    }
}