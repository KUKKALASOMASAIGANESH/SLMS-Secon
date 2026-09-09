using System.ComponentModel.DataAnnotations;

namespace SLMS.WebApp.Models.DigitalLibrary;

public class AdminPolicyViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "PolicyTitle is required")]
    public string PolicyTitle { get; set; }
        = string.Empty;

    [Required(ErrorMessage = "PolicyContent is required")]
    public string PolicyContent { get; set; }
        = string.Empty;
}