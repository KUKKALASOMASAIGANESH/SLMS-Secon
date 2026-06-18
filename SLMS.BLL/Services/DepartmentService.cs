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
        var entity =
            _mapper.Map<Department>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            DepartmentResponseDto>(entity);
    }
}