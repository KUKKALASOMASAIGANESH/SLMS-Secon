namespace SLMS.Shared.DTOs.Department;

public class DepartmentUpdateDto
{
    public int Id { get; set; }

    public string DepartmentCode { get; set; } = string.Empty;

    public string DepartmentName { get; set; } = string.Empty;

    public string? Description { get; set; }
}