using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class RequestRepository
    : Repository<Request>,
      IRequestRepository
{
    public RequestRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}