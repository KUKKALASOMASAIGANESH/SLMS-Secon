











using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.Models;

public class DepartmentViewModel
{
    public int Id { get; set; }
    [RegularExpression(@"^[A-Z]{2,5}[0-9]{3}$",
    ErrorMessage = "Department Code format should be like IT001 or HR001")]
    public string DepartmentCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "Department Name is required")]
    [StringLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Description { get; set; }
}