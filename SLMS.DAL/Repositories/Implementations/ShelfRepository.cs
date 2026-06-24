using Microsoft.EntityFrameworkCore;

using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class ShelfRepository : IShelfRepository
{
    private readonly SLMSDbContext _context;

    public ShelfRepository(
        SLMSDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shelf>>
        GetAllAsync()
    {
        return await _context.Shelves
            .ToListAsync();
    }

    public async Task<Shelf?>
        GetByIdAsync(int id)
    {
        return await _context.Shelves
            .FirstOrDefaultAsync(
                x => x.Id == id);
    }

    public async Task AddAsync(
        Shelf shelf)
    {
        await _context.Shelves
            .AddAsync(shelf);
    }

    public void Update(
        Shelf shelf)
    {
        _context.Shelves
            .Update(shelf);
    }

    public void Delete(
    Shelf shelf)
    {
        _context.Shelves
            .Remove(shelf);
    }

    public async Task SaveChangesAsync()
    {
        await _context
            .SaveChangesAsync();
    }
}