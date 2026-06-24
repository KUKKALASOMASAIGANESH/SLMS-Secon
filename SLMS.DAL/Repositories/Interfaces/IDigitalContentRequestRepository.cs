using SLMS.DOL.Entities;

namespace SLMS.DAL.Repositories.Interfaces;

public interface IDigitalContentRequestRepository
    : IRepository<DigitalContentRequest>
{
    Task<IEnumerable<DigitalContentRequest>>
        GetByEmployeeIdAsync(int employeeId);

    Task<IEnumerable<DigitalContentRequest>>
    GetAllWithDetailsAsync();
}