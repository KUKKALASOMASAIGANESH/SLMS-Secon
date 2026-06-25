using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Interfaces;
using SLMS.Shared.DTOs;

namespace SLMS.WebAPI.Controllers;

// API Controller responsible for authentication-related operations
// (Register, Login, Forgot Password)
// This layer communicates with the Business Logic Layer (BLL)
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // Inject authentication service from Business Layer
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Registers a new user in the system
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        // If registration fails, return HTTP 400 with error message
        if (!result.Success)
            return BadRequest(result);

        // Return success response
        return Ok(result);
    }

    // Authenticates user and returns JWT token
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        // If login fails, return HTTP 400
        if (!result.Success)
            return BadRequest(result);

        // Return JWT token and success status
        return Ok(result);
    }

    // Handles password reset request
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var result = await _authService.ForgotPasswordAsync(dto);

        // If operation fails, return HTTP 400
        if (!result.Success)
            return BadRequest(result);

        // Return success response
        return Ok(result);
    }
}