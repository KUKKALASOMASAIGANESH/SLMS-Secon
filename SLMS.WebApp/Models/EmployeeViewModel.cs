using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SLMS.WebApp.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Employee Number is required")]
    [RegularExpression(@"^EMP\d+$",
    ErrorMessage = "Employee Number must start with EMP followed by numbers")]
    public string EmployeeNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [RegularExpression(@"^[0-9]{10}$",
 ErrorMessage = "Phone Number must be exactly 10 digits")]
    public string Phone { get; set; } = string.Empty;
    [Required(ErrorMessage = "Designation is required")]
    public string Designation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    public int DepartmentId { get; set; }

    public List<SelectListItem> Departments
    {
        get; set;
    } = new();
   
}