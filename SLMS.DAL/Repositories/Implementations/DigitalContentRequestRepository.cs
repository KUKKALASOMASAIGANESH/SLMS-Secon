using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class DigitalContentRequestRepository
    : Repository<DigitalContentRequest>,
      IDigitalContentRequestRepository
{
    public DigitalContentRequestRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}