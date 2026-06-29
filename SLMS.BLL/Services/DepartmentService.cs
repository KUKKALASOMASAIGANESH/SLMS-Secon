using AutoMapper;

using SLMS.BLL.Interfaces;

using SLMS.DAL.Repositories.Interfaces;

using SLMS.DOL.Entities;

using SLMS.Shared.DTOs.Department;

namespace SLMS.BLL.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    private readonly IMapper _mapper;

    public DepartmentService(
        IDepartmentRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DepartmentResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<DepartmentResponseDto>>
            (entities);
    }

    public async Task<DepartmentResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            DepartmentResponseDto>(entity);
    }
    public async Task<DepartmentResponseDto>
        CreateAsync(DepartmentCreateDto dto)
    {
        var departments =
            await _repository.GetAllAsync();

        if (departments.Any(x =>
            x.DepartmentCode.ToLower() ==
            dto.DepartmentCode.ToLower()))
        {
            throw new Exception(
                "Department Code already exists");
        }

        if (departments.Any(x =>
            x.DepartmentName.ToLower() ==
            dto.DepartmentName.ToLower()))
        {
            throw new Exception(
                "Department Name already exists");
        }

        var entity =
            _mapper.Map<Department>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            DepartmentResponseDto>(entity);
    }
    public async Task<DepartmentResponseDto?>
    UpdateAsync(
        int id,
        DepartmentUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.DepartmentCode =
            dto.DepartmentCode;

        entity.DepartmentName =
            dto.DepartmentName;

        entity.Description =
            dto.Description;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            DepartmentResponseDto>(entity);
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
    public async Task<IEnumerable<DepartmentResponseDto>>
    SearchAsync(string keyword)
    {
        var departments =
            await _repository.SearchAsync(keyword);

        return _mapper.Map<
            IEnumerable<DepartmentResponseDto>>
            (departments);
    }
}