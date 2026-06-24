using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.Shared.DTOs.User;

namespace SLMS.WebAPI.Controllers;


[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class UserManagementController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserManagementController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetAllUsersWithRolesAsync();

        var result = users.Select(u => new UserRoleDto
        {
            UserId = u.Id,
            Username = u.Username,
            EmployeeName = u.Employee.FullName,
            RoleName = u.UserRoles.FirstOrDefault()?.Role.RoleName ?? "No Role"
        }).ToList();

        return Ok(result);
    }

    [HttpPost("change-role")]
    public async Task<IActionResult> ChangeRole(int userId, int roleId)
    {
        await _userRepository.ChangeUserRoleAsync(userId, roleId);

        return Ok(new
        {
            success = true,
            message = "Role updated successfully"
        });
    }
}