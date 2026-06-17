namespace SLMS.Shared.DTOs.DigitalLibrary;

public class PolicyResponseDto
{
    public int Id { get; set; }

    public string PolicyTitle { get; set; } = string.Empty;

    public string PolicyContent { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}