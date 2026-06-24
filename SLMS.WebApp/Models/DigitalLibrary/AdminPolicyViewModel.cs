namespace SLMS.WebApp.Models.DigitalLibrary;

public class AdminPolicyViewModel
{
    public int Id { get; set; }

    public string PolicyTitle { get; set; }
        = string.Empty;

    public string PolicyContent { get; set; }
        = string.Empty;
}