

using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs.Inventory;

namespace SLMS.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(
        IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
   public async Task<IActionResult> GetAll()
    {
     var result = await _inventoryService.GetAllAsync();

     return Ok(result);
     }

    [HttpPost]
    public async Task<IActionResult> Create(
    CreateInventoryItemDto dto)
    {
        await _inventoryService.CreateAsync(dto);

        return Ok("Inventory Item Created Successfully");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var inventoryItem = await _inventoryService.GetByIdAsync(id);

        if (inventoryItem == null)
            return NotFound();

        return Ok(inventoryItem);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateInventoryItemDto dto)
    {
        var updated =
            await _inventoryService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return Ok("Inventory Item Updated Successfully");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _inventoryService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return Ok("Inventory Item Deleted Successfully");
    }


}