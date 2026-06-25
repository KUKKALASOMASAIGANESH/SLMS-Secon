namespace SLMS.Shared.DTOs.Auth;

// Data Transfer Object used for Login request
// This class carries user credentials from frontend to backend API
public class LoginDto
{
    // Username entered by the user during login
    public string Username { get; set; } = string.Empty;

    // Password entered by the user during login
    public string Password { get; set; } = string.Empty;
}