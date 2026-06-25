namespace SLMS.Shared.DTOs.Auth;

// DTO returned from API after successful login
// Contains JWT token and basic user information
public class LoginResponseDto
{
    // JWT token generated after successful authentication
    public string Token { get; set; } = string.Empty;

    // Username of the logged-in user
    public string UserName { get; set; } = string.Empty;

    // Role of the user (e.g., Admin, User, Librarian)
    public string Role { get; set; } = string.Empty;
}