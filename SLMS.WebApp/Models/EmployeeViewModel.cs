using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Employee Number is required")]
    public string EmployeeNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Designation is required")]
    public string Designation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    public int DepartmentId { get; set; }
}