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

    private readonly IMapper _mapper;

    public LibraryResourceService(
        ILibraryResourceRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<LibraryResourceResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<LibraryResourceResponseDto>>
            (entities);
    }

    public async Task<
        LibraryResourceResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            LibraryResourceResponseDto>
            (entity);
    }

    public async Task<
        LibraryResourceResponseDto>
        CreateAsync(
            LibraryResourceCreateDto dto)
    {
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

        entity.CategoryId = dto.CategoryId;
        entity.ResourceType = dto.ResourceType;
        entity.Title = dto.Title;
        entity.Author = dto.Author;
        entity.Publisher = dto.Publisher;
        entity.ISBN = dto.ISBN;
        entity.PublicationYear = dto.PublicationYear;

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