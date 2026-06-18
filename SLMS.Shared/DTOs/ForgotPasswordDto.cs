namespace SLMS.Shared.DTOs;

public class ForgotPasswordDto
{
    public string EmployeeNumber { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}