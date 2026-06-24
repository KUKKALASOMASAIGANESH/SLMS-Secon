using SLMS.DOL.Entities;

public interface IDigitalContentService
{
    Task<IEnumerable<DigitalContent>> GetAllAsync();

    Task<DigitalContent?> GetByIdAsync(int id);

    Task AddAsync(DigitalContent digitalContent);

    Task UpdateAsync(DigitalContent digitalContent);

    Task DeleteAsync(int id);
}