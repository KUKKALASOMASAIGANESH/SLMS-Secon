using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class UserRepository
    : Repository<User>, IUserRepository
{
    public UserRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}