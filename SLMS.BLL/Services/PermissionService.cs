using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Permission;

namespace SLMS.BLL.Services;

public class PermissionService
    : IPermissionService
{
    private readonly IPermissionRepository _repository;

    private readonly IMapper _mapper;

    public PermissionService(
        IPermissionRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PermissionResponseDto>>
        GetAllAsync()
    {
        var permissions =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<PermissionResponseDto>>
            (permissions);
    }

    public async Task<PermissionResponseDto?>
        GetByIdAsync(int id)
    {
        var permission =
            await _repository.GetByIdAsync(id);

        if (permission == null)
            return null;

        return _mapper.Map<
            PermissionResponseDto>(permission);
    }

    public async Task<PermissionResponseDto>
        CreateAsync(
            PermissionCreateDto dto)
    {
        var permission =
            _mapper.Map<Permission>(dto);

        await _repository.AddAsync(permission);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            PermissionResponseDto>(permission);
    }

    public async Task<PermissionResponseDto?>
        UpdateAsync(
            int id,
            PermissionUpdateDto dto)
    {
        var permission =
            await _repository.GetByIdAsync(id);

        if (permission == null)
            return null;

        _mapper.Map(dto, permission);

        _repository.Update(permission);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            PermissionResponseDto>(permission);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        var permission =
            await _repository.GetByIdAsync(id);

        if (permission == null)
            return false;

        _repository.Delete(permission);

        await _repository.SaveChangesAsync();

        return true;
    }
}