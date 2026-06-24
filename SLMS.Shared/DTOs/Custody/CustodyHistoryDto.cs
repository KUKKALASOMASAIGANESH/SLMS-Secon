namespace SLMS.Shared.DTOs.Custody;

public class CustodyHistoryDto
{
    public int Id { get; set; }

    public int InventoryItemId { get; set; }

    public int? FromDepartmentId { get; set; }

    public int ToDepartmentId { get; set; }

    public DateTime TransferDate { get; set; }

    public string? TransferReason { get; set; }

    public string? Remarks { get; set; }
    public string FromDepartmentName { get; set; } = string.Empty;

    public string ToDepartmentName { get; set; } = string.Empty;

    public int? TransferredByUserId { get; set; }
}