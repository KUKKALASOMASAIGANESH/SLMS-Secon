/*using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Employee Number is required")]
    public string EmployeeNumber { get; set; } = string.Empty;





    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters and contain uppercase, lowercase, number and special character.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Compare("Password",
        ErrorMessage = "Password and Confirm Password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}*/


using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Employee Number is required")]
    public string EmployeeNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee Name is required")]
    public string EmployeeName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters and contain uppercase, lowercase, number and special character.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}