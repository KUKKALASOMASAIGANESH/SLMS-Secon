using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;

namespace SLMS.BLL.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IAuditLogService _auditLogService;
    public EmployeeService(
    IEmployeeRepository repository,
    IAuditLogService auditLogService)
    {
        _repository = repository;
        _auditLogService = auditLogService;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Employee employee)
    {
       await _repository.AddAsync(employee);
        await _repository.SaveChangesAsync();
    //    await _auditLogService.AddAsync(
    //    new AuditLog
    //    {
    //        UserId = 3,
    //        Module = "Employee",
    //        Action = "Create",
    //        NewValue = employee.FullName
    //    });
    }
    public async Task<IEnumerable<Employee>>
    SearchByNameAsync(string name)
    {
        return await _repository
            .SearchByNameAsync(name);
    }
    public async Task UpdateAsync(Employee employee)
    {
        _repository.Update(employee);
        await _repository.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var employee =
            await _repository.GetByIdAsync(id);

        if (employee != null)
        {
            _repository.Delete(employee);

            await _repository.SaveChangesAsync();
        }
    }
}