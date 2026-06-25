using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebApp.Services;

namespace SLMS.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class UserManagementController : Controller
{
    private readonly UserManagementService _service;

    public UserManagementController(UserManagementService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _service.GetAllAsync();

        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
   
    public async Task<IActionResult> ChangeRole(int userId, int roleId)
    {
        var currentUsername = User.Identity?.Name;

        var users = await _service.GetAllAsync();

        var targetUser = users.FirstOrDefault(u => u.UserId == userId);

        if (targetUser == null)
        {
            TempData["Error"] = "User not found.";
            return RedirectToAction(nameof(Index));
        }

        if (targetUser.Username == currentUsername && roleId != 1)
        {
            TempData["Error"] = "You cannot remove your own Admin role.";
            return RedirectToAction(nameof(Index));
        }

        var success = await _service.ChangeRoleAsync(userId, roleId);

        TempData[success ? "Success" : "Error"] =
            success ? "Role updated successfully." : "Unable to update role.";

        return RedirectToAction(nameof(Index));
    }
}