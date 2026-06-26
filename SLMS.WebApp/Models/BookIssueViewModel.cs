using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.Models;

public class BookIssueViewModel
{
    // Primary Key
    public int Id { get; set; }

    // Book Selection
    [Required(ErrorMessage = "Book Title is required")]
    public int LibraryResourceId { get; set; }

    // Employee Selection
    [Required(ErrorMessage = "Employee is required")]
    public int EmployeeId { get; set; }

    // Display Book Title
    public string BookTitle { get; set; } = string.Empty;

    // Display Employee Name
    public string EmployeeName { get; set; } = string.Empty;

    // Issue Date
    [Required(ErrorMessage = "Issue Date is required")]
    [DataType(DataType.Date)]
    public DateTime IssueDate { get; set; }

    // Due Date
    [Required(ErrorMessage = "Due Date is required")]
    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }

    // Issued User
    public int IssuedByUserId { get; set; }

    // Book Status
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = string.Empty;

    // Employee Dropdown
    public List<SelectListItem> EmployeeList { get; set; }
        = new();

    // Book Dropdown
    public List<SelectListItem> BookList { get; set; }
        = new();
}