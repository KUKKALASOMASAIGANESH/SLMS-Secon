namespace SLMS.Shared.DTOs.User;

public class UserCreateDto
{
    public int EmployeeId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
}