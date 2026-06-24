using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IUserRepository
    : IRepository<User>
{
   
    Task<User?> GetUserWithRolesAsync(string username);

    Task AssignRoleAsync(int userId, int roleId);

    Task<List<User>> GetAllUsersWithRolesAsync();

    Task ChangeUserRoleAsync(int userId, int roleId);

}