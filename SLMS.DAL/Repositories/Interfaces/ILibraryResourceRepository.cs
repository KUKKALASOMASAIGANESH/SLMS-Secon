using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface ILibraryResourceRepository
    : IRepository<LibraryResource>
{
    Task<IEnumerable<LibraryResource>>
        SearchAsync(string keyword);
}