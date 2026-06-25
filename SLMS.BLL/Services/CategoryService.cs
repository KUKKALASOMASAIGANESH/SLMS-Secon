using AutoMapper;
using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs.Category;

namespace SLMS.BLL.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponseDto>>
        GetAllAsync()
    {
        var entities =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<CategoryResponseDto>>
            (entities);
    }

    public async Task<CategoryResponseDto?>
        GetByIdAsync(int id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return _mapper.Map<
            CategoryResponseDto>(entity);
    }

    public async Task<CategoryResponseDto>
        CreateAsync(
            CategoryCreateDto dto)
    {
        var entity =
            _mapper.Map<Category>(dto);

        await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            CategoryResponseDto>(entity);
    }

    public async Task<CategoryResponseDto?>
        UpdateAsync(
            int id,
            CategoryUpdateDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        entity.Name = dto.Name;
        entity.Description = dto.Description;

        await _repository.SaveChangesAsync();

        return _mapper.Map<
            CategoryResponseDto>(entity);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        var hasResources =
    await _repository
        .HasResourcesAsync(id);

        if (hasResources)
        {
            throw new Exception(
                "Cannot delete category because resources are assigned to it.");
        }

        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        _repository.Delete(entity);

        await _repository.SaveChangesAsync();

        return true;
    }
}