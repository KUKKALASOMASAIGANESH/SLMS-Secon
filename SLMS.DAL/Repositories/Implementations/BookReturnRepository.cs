using SLMS.DAL.Data;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Implementations;

public class BookReturnRepository
    : Repository<BookReturn>,
      IBookReturnRepository
{
    public BookReturnRepository(
        SLMSDbContext context)
        : base(context)
    {
    }
}