using SLMS.WebApp.Models.Inventory;

namespace SLMS.WebApp.Services.Interfaces;

public interface IShelfService
{
    Task<List<ShelfViewModel>> GetAllAsync();

    Task<ShelfViewModel?> GetByIdAsync(int id);

    Task CreateAsync(
        CreateShelfViewModel model);

    Task UpdateAsync(
        int id,
        UpdateShelfViewModel model);

    Task DeleteAsync(int id);
}
