namespace SLMS.Shared.DTOs;

// DTO used for user registration request
// Sent from frontend to API when creating a new user account
public class RegisterDto
{
    // Unique employee number used for identifying the user in the system
    public string EmployeeNumber { get; set; } = string.Empty;


    public string EmployeeName { get; set; } = string.Empty;


    // Username chosen by the user for login
    public string Username { get; set; } = string.Empty;

    // Password for the new account
    public string Password { get; set; } = string.Empty;


}