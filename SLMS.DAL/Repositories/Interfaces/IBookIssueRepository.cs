using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IBookIssueRepository
    : IRepository<BookIssue>
{
    Task<IEnumerable<BookIssue>>
        GetOverdueBooksAsync();

    Task<IEnumerable<BookIssue>>
        GetAllWithDetailsAsync();

    Task<BookIssue?>
        GetByIdWithDetailsAsync(int id);
}