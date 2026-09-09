using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Employee;

namespace SLMS.BLL.Services;

public class EmployeeService
    : IEmployeeService
{
    private readonly IEmployeeRepository
        _repository;

    private readonly IMapper _mapper;

    public EmployeeService(
        IEmployeeRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<EmployeeResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<EmployeeResponseDto>>
            (entities);
    }

    public async Task<
        EmployeeResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            EmployeeResponseDto>
            (entity);
    }

    public async Task<EmployeeResponseDto>
     CreateAsync(EmployeeCreateDto dto)
    {
        var existingEmployee =
            await _repository.FindAsync(e =>
                e.EmployeeNumber == dto.EmployeeNumber);

        if (existingEmployee.Any())
        {
            throw new Exception(
                "Employee Number already exists.");
        }

        var entity =
            _mapper.Map<Employee>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<EmployeeResponseDto>(entity);
    }

    public async Task<
        EmployeeResponseDto?>
        UpdateAsync(
            int id,
            EmployeeUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;
        var duplicateEmployee =
    await _repository.FindAsync(e =>
        e.EmployeeNumber == dto.EmployeeNumber
        && e.Id != id);

        if (duplicateEmployee.Any())
        {
            throw new Exception(
                "Employee Number already exists.");
        }

        entity.EmployeeNumber =
            dto.EmployeeNumber;

        entity.FullName =
            dto.FullName;

        entity.Email =
            dto.Email;

        entity.Phone =
            dto.Phone;

        entity.Designation =
            dto.Designation;

        entity.DepartmentId =
            dto.DepartmentId;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            EmployeeResponseDto>
            (entity);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        _repository.Delete(entity);

        await _repository.SaveChangesAsync();

        return true;
    }
    public async Task<IEnumerable<Employee>>
    SearchByNameAsync(string name)
    {
        return await _repository.SearchByNameAsync(name);
    }
}