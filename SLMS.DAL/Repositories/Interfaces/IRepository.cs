/*
 * IRepository<T>
 *
 * Purpose:
 * Defines common data access operations shared across all entities.
 *
 * Responsibilities:
 * - Retrieve records by identifier
 * - Retrieve all records
 * - Filter records using expressions
 * - Create new records
 * - Update existing records
 * - Delete records
 * - Persist changes to the database
 *
 * Architecture Flow:
 * Service Layer
 *       ↓
 * IRepository<T>
 *       ↓
 * Repository<T>
 *       ↓
 * Entity Framework Core
 *       ↓
 * PostgreSQL Database
 *
 * This generic repository provides reusable CRUD operations
 * and serves as the foundation for module-specific repositories
 * such as InventoryRepository, ResourceRepository, EmployeeRepository,
 * and other data access implementations.
 */

using System.Linq.Expressions;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task SaveChangesAsync();
}