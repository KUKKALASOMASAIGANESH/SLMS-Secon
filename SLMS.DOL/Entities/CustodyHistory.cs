using SLMS.DOL.Common;

namespace SLMS.DOL.Entities;

public class CustodyHistory : BaseEntity
{
    public int InventoryItemId { get; set; }

    public int? FromDepartmentId { get; set; }

    public int ToDepartmentId { get; set; }

    public DateTime TransferDate { get; set; }

    public string? TransferReason { get; set; }

    public string? Remarks { get; set; }

    public int? TransferredByUserId { get; set; }

    public InventoryItem? InventoryItem { get; set; } 

    public Department? FromDepartment { get; set; }

    public Department? ToDepartment { get; set; } 

    public User? TransferredByUser { get; set; }
}