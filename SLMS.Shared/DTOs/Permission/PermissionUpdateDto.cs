namespace SLMS.Shared.DTOs.Permission;

public class PermissionUpdateDto
{
    public string PermissionName { get; set; } = string.Empty;

    public string? Description { get; set; }
}