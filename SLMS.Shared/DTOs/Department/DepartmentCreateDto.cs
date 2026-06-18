namespace SLMS.Shared.DTOs.Department;

public class DepartmentCreateDto
{
    public string DepartmentCode { get; set; } = string.Empty;

    public string DepartmentName { get; set; } = string.Empty;

    public string? Description { get; set; }
}