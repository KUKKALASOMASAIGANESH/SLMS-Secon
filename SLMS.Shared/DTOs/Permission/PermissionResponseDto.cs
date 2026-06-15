namespace SLMS.Shared.DTOs.Permission;

public class PermissionResponseDto
{
    public int Id { get; set; }

    public string PermissionName { get; set; } = string.Empty;

    public string? Description { get; set; }
}