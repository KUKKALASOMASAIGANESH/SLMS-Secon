using AutoMapper;

using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Shelf;

namespace SLMS.BLL.Services;

public class ShelfService
    : IShelfService
{
    private readonly IShelfRepository
        _repository;

    private readonly IMapper
        _mapper;

    public ShelfService(
        IShelfRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<ShelfResponseDto>>
        GetAllAsync()
    {
        var shelves =
            await _repository
                .GetAllAsync();

        return _mapper.Map<
            IEnumerable<ShelfResponseDto>>
            (shelves);
    }

    public async Task<
        ShelfResponseDto?>
        GetByIdAsync(int id)
    {
        var shelf =
            await _repository
                .GetByIdAsync(id);

        if (shelf == null)
            return null;

        return _mapper.Map<
            ShelfResponseDto>
            (shelf);
    }

    public async Task<
        ShelfResponseDto>
        CreateAsync(
            ShelfCreateDto dto)
    {
        var shelf =
            _mapper.Map<Shelf>(dto);

        await _repository
            .AddAsync(shelf);

        await _repository
            .SaveChangesAsync();

        return _mapper.Map<
            ShelfResponseDto>
            (shelf);
    }

    public async Task<
        ShelfResponseDto?>
        UpdateAsync(
            int id,
            ShelfUpdateDto dto)
    {
        var shelf =
            await _repository
                .GetByIdAsync(id);

        if (shelf == null)
            return null;

        shelf.ShelfName =
            dto.ShelfName;

        shelf.Capacity =
            dto.Capacity;

        shelf.CurrentBookCount =
            dto.CurrentBookCount;

        _repository.Update(shelf);

        await _repository
            .SaveChangesAsync();

        return _mapper.Map<
            ShelfResponseDto>
            (shelf);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        var shelf =
            await _repository
                .GetByIdAsync(id);

        if (shelf == null)
            return false;

        _repository.Delete(shelf);

        await _repository
            .SaveChangesAsync();

        return true;
    }
}