namespace SLMS.Shared.DTOs;

// DTO used to send login credentials from UI to API
// This is part of the authentication request payload
public class LoginDto
{
    // Username entered by the user
    public string Username { get; set; } = string.Empty;

    // Password entered by the user
    public string Password { get; set; } = string.Empty;
}