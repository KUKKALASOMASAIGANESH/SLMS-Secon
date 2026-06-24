using Microsoft.EntityFrameworkCore;
using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class UserRepository
    : Repository<User>,
      IUserRepository
{
    private readonly SLMSDbContext _context;

    public UserRepository(SLMSDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetUserWithRolesAsync(string username)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(
                u => u.Username.ToLower() == username.ToLower());
    }

    public async Task AssignRoleAsync(int userId, int roleId)
    {
        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };

        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsersWithRolesAsync()
    {
        return await _context.Users
            .Include(u => u.Employee)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task ChangeUserRoleAsync(int userId, int roleId)
    {
        var existingRoles = _context.UserRoles
            .Where(ur => ur.UserId == userId);

        _context.UserRoles.RemoveRange(existingRoles);

        await _context.UserRoles.AddAsync(new UserRole
        {
            UserId = userId,
            RoleId = roleId
        });

        await _context.SaveChangesAsync();
    }
}