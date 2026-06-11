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
}