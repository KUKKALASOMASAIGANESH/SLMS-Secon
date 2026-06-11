using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class LibraryResourceRepository
    : Repository<LibraryResource>,
      ILibraryResourceRepository
{
    public LibraryResourceRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}