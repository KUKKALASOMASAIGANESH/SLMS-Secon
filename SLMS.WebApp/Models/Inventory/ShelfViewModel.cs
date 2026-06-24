namespace SLMS.WebApp.Models.Inventory;

public class ShelfViewModel
{
    public int Id { get; set; }

    public string ShelfName { get; set; }
        = string.Empty;

    public int Capacity { get; set; }

    public int CurrentBookCount { get; set; }

    public bool IsActive { get; set; }

    public List<ShelfResourceViewModel>
  Resources
    { get; set; }
  = new();
    public int RemainingCapacity
    {
        get
        {
            return Capacity - CurrentBookCount;
        }
    }


  
}