using SLMS.Shared.DTOs;
using SLMS.Shared.Responses;

namespace SLMS.BLL.Interfaces;

// Contract for Authentication Business Logic Layer
// Defines all authentication-related operations used by API layer
public interface IAuthService
{
    // Handles user login validation and returns JWT token + user info
    Task<AuthResponse> LoginAsync(LoginDto dto);

    // Registers a new user into the system
    Task<AuthResponse> RegisterAsync(RegisterDto dto);

    // Allows user to reset password using employee details
    Task<AuthResponse> ForgotPasswordAsync(ForgotPasswordDto dto);
}