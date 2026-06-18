namespace SLMS.Shared.DTOs.Custody;

public class AuditLogDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Module { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? IPAddress { get; set; }
}