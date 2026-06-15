using AutoMapper;

using SLMS.BLL.Interfaces;

using SLMS.DAL.Repositories.Interfaces;

using SLMS.DOL.Entities;

using SLMS.Shared.DTOs.User;

namespace SLMS.BLL.Services;

public class UserService
    : IUserService
{
    private readonly IUserRepository
        _repository;

    private readonly IMapper
        _mapper;

    public UserService(
        IUserRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<UserResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<UserResponseDto>>
            (entities);
    }

    public async Task<
        UserResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            UserResponseDto>
            (entity);
    }

    public async Task<
        UserResponseDto>
        CreateAsync(
            UserCreateDto dto)
    {
        var entity =
            _mapper.Map<User>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            UserResponseDto>
            (entity);
    }

    public async Task<
        UserResponseDto?>
        UpdateAsync(
            int id,
            UserUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.EmployeeId =
            dto.EmployeeId;

        entity.Username =
            dto.Username;

        entity.PasswordHash =
            dto.PasswordHash;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            UserResponseDto>
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
}