namespace SLMS.Shared.DTOs.Permission;

public class PermissionCreateDto
{
    public string PermissionName { get; set; } = string.Empty;

    public string? Description { get; set; }
}