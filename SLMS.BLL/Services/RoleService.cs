using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Role;

namespace SLMS.BLL.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public RoleService(
        IRoleRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleResponseDto>>
        GetAllAsync()
    {
        var roles =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<RoleResponseDto>>(roles);
    }

    public async Task<RoleResponseDto?>
        GetByIdAsync(int id)
    {
        var role =
            await _repository.GetByIdAsync(id);

        if (role == null)
            return null;

        return _mapper.Map<RoleResponseDto>(role);
    }

    public async Task<RoleResponseDto>
        CreateAsync(RoleCreateDto dto)
    {
        var role =
            _mapper.Map<Role>(dto);

        await _repository.AddAsync(role);

        await _repository.SaveChangesAsync();

        return _mapper.Map<RoleResponseDto>(role);
    }

    public async Task<RoleResponseDto?>
        UpdateAsync(
            int id,
            RoleUpdateDto dto)
    {
        var role =
            await _repository.GetByIdAsync(id);

        if (role == null)
            return null;

        _mapper.Map(dto, role);

        _repository.Update(role);

        await _repository.SaveChangesAsync();

        return _mapper.Map<RoleResponseDto>(role);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        var role =
            await _repository.GetByIdAsync(id);

        if (role == null)
            return false;

        _repository.Delete(role);

        await _repository.SaveChangesAsync();

        return true;
    }
}