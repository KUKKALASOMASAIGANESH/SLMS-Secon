using AutoMapper;

using SLMS.BLL.Interfaces;

using SLMS.DAL.Repositories.Interfaces;

using SLMS.DOL.Entities;

using SLMS.Shared.DTOs.LibraryResource;

namespace SLMS.BLL.Services;

public class LibraryResourceService
    : ILibraryResourceService
{
    private readonly ILibraryResourceRepository
        _repository;
    private readonly IShelfRepository _shelfRepository;

    private readonly IMapper _mapper;

    public LibraryResourceService(
      ILibraryResourceRepository repository,
      IShelfRepository shelfRepository,
      IMapper mapper)
    {
        _repository = repository;
        _shelfRepository = shelfRepository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<LibraryResourceResponseDto>>
        GetAllAsync()
    {
        var entities =
        await _repository.GetAllWithShelfAsync();

        return _mapper.Map<
            IEnumerable<LibraryResourceResponseDto>>
            (entities);
    }

    public async Task<
        LibraryResourceResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
    await _repository.GetByIdWithShelfAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            LibraryResourceResponseDto>
            (entity);
    }
    
        public async Task<LibraryResourceResponseDto>
    CreateAsync(
        LibraryResourceCreateDto dto)
        {
            // Check shelf capacity first
            if (dto.ShelfId.HasValue)
            {
                var shelf =
                    await _shelfRepository
                        .GetByIdAsync(dto.ShelfId.Value);

                if (shelf != null)
                {
                    if (shelf.CurrentBookCount >= shelf.Capacity)
                    {
                        throw new Exception(
                            "Selected shelf is full.");
                    }

                    shelf.CurrentBookCount++;

                    _shelfRepository.Update(shelf);
                }
            }

            var entity =
                _mapper.Map<LibraryResource>(dto);

            await _repository.AddAsync(entity);

            await _repository.SaveChangesAsync();

            return _mapper.Map<
                LibraryResourceResponseDto>
                (entity);
        }

    

    public async Task<
    LibraryResourceResponseDto?>
    UpdateAsync(
        int id,
        LibraryResourceUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        // Shelf changed
        if (entity.ShelfId != dto.ShelfId)
        {
            // Old Shelf Count --
            if (entity.ShelfId.HasValue)
            {
                var oldShelf =
                    await _shelfRepository
                        .GetByIdAsync(entity.ShelfId.Value);

                if (oldShelf != null)
                {
                    oldShelf.CurrentBookCount--;

                    _shelfRepository.Update(oldShelf);
                }
            }

            // New Shelf Count ++
            if (dto.ShelfId.HasValue)
            {
                var newShelf =
                    await _shelfRepository
                        .GetByIdAsync(dto.ShelfId.Value);

                if (newShelf != null)
                {
                    if (newShelf.CurrentBookCount >=
                        newShelf.Capacity)
                    {
                        throw new Exception(
                            "Selected shelf is full.");
                    }

                    newShelf.CurrentBookCount++;

                    _shelfRepository.Update(newShelf);
                }
            }
        }

        entity.CategoryId = dto.CategoryId;
        entity.ResourceType = dto.ResourceType;
        entity.Title = dto.Title;
        entity.Author = dto.Author;
        entity.Publisher = dto.Publisher;
        entity.ISBN = dto.ISBN;
        entity.PublicationYear = dto.PublicationYear;
        entity.ShelfId = dto.ShelfId;

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            LibraryResourceResponseDto>
            (entity);
    }

    public async Task<bool>
    DeleteAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        // Reduce Shelf Count
        if (entity.ShelfId.HasValue)
        {
            var shelf =
                await _shelfRepository
                    .GetByIdAsync(entity.ShelfId.Value);

            if (shelf != null &&
                shelf.CurrentBookCount > 0)
            {
                shelf.CurrentBookCount--;

                _shelfRepository.Update(shelf);
            }
        }

        _repository.Delete(entity);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<
        IEnumerable<LibraryResourceResponseDto>>
        SearchAsync(string keyword)
    {
        var entities =
            await _repository.SearchAsync(keyword);

        return _mapper.Map<
            IEnumerable<LibraryResourceResponseDto>>
            (entities);
    }
}