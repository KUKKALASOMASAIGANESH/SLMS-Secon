using SLMS.DOL.Entities;

namespace SLMS.BLL.Interfaces;

public interface IDigitalContentRequestService
{
    Task<IEnumerable<DigitalContentRequest>> GetAllAsync();

    Task<DigitalContentRequest?> GetByIdAsync(int id);

    Task AddAsync(DigitalContentRequest request);

    Task UpdateAsync(
    DigitalContentRequest request);

    Task<IEnumerable<DigitalContentRequest>>
    GetByEmployeeIdAsync(int employeeId);

    Task<IEnumerable<DigitalContentRequest>>
    GetAllWithDetailsAsync();
}