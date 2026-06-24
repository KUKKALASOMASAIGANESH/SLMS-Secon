using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace SLMS.DAL.Repositories.Implementations;

public class Repository<T> : IRepository<T>
    where T : class
{
    protected readonly SLMSDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(SLMSDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate)
    {
        return await _dbSet
            .Where(predicate)
            .ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}