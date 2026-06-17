using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.Models;

public class CustodyHistoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Inventory Item Id is required")]
    [Range(1, int.MaxValue,
        ErrorMessage = "Inventory Item Id must be greater than 0")]
    public int InventoryItemId { get; set; }

    public int? FromDepartmentId { get; set; }

    [Required(ErrorMessage = "To Department is required")]
    [Range(1, int.MaxValue,
        ErrorMessage = "Department Id must be greater than 0")]
    public int ToDepartmentId { get; set; }

    [Required(ErrorMessage = "Transfer Date is required")]
    public DateTime TransferDate { get; set; }

    [StringLength(250,
        ErrorMessage = "Transfer Reason cannot exceed 250 characters")]
    public string? TransferReason { get; set; }

    [StringLength(500,
        ErrorMessage = "Remarks cannot exceed 500 characters")]
    public string? Remarks { get; set; }

    public int? TransferredByUserId { get; set; }
}