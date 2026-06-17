using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class PolicyRepository
    : Repository<Policy>,
      IPolicyRepository
{
    public PolicyRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}