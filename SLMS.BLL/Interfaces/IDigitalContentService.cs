using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface IDigitalContentService
{
    Task<IEnumerable<DigitalContent>> GetAllAsync();

    Task<DigitalContent?> GetByIdAsync(int id);

    Task AddAsync(DigitalContent digitalContent);
}