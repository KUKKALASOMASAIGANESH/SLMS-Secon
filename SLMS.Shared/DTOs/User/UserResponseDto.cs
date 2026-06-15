namespace SLMS.Shared.DTOs.User;

public class UserResponseDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime? LastLoginDate { get; set; }
}