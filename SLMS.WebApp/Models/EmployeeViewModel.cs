using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SLMS.WebApp.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Employee Number is required.")]
    [Display(Name = "Employee Number")]
    [RegularExpression(
        @"^EMP\d+$",
        ErrorMessage = "Employee Number must start with 'EMP' followed by numbers."
    )]
    public string EmployeeNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full Name is required.")]
    [Display(Name = "Full Name")]
    [StringLength(
        100,
        ErrorMessage = "Full Name cannot exceed 100 characters."
    )]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [Display(Name = "Email Address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required.")]
    [Display(Name = "Phone Number")]
    [RegularExpression(
        @"^[0-9]{10}$",
        ErrorMessage = "Phone Number must contain exactly 10 digits."
    )]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Designation is required.")]
    [Display(Name = "Designation")]
    public string Designation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required.")]
    [Display(Name = "Department")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a department.")]
    public int DepartmentId { get; set; }

    public List<SelectListItem> Departments { get; set; } = new();
    public string? DepartmentName { get; set; }
}