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
                .GetByIdWithResourcesAsync(id);

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
        var existingShelf =
            (await _repository.GetAllAsync())
            .FirstOrDefault(x =>
                x.ShelfName.ToLower() ==
                dto.ShelfName.ToLower());

        if (existingShelf != null)
        {
            throw new Exception(
                "Shelf name already exists.");
        }

        var shelf =
            _mapper.Map<Shelf>(dto);

        // Always start with 0
        shelf.CurrentBookCount = 0;

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

        if (dto.Capacity < shelf.CurrentBookCount)
        {
            throw new Exception(
                $"Capacity cannot be less than current book count ({shelf.CurrentBookCount}).");
        }

        var duplicateShelf =
            (await _repository.GetAllAsync())
            .FirstOrDefault(x =>
                x.Id != id &&
                x.ShelfName.ToLower() ==
                dto.ShelfName.ToLower());

        if (duplicateShelf != null)
        {
            throw new Exception(
                "Shelf name already exists.");
        }

        shelf.ShelfName =
            dto.ShelfName;

        shelf.Capacity =
            dto.Capacity;

        // Don't allow manual count updates
        // shelf.CurrentBookCount = dto.CurrentBookCount;

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

        var hasResources =
            await _repository
                .HasResourcesAsync(id);

        if (hasResources)
        {
            throw new Exception(
                "Cannot delete shelf because resources are assigned to it.");
        }

        _repository.Delete(shelf);

        await _repository
            .SaveChangesAsync();

        return true;
    }
}