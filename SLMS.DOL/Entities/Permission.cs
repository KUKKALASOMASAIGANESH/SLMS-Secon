using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class Permission : BaseEntity
{
    public string PermissionName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();
}