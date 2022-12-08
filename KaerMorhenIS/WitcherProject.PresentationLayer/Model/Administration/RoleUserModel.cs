using System.ComponentModel.DataAnnotations;

namespace WitcherProject.PresentationLayer.Model.Administration;

public class RoleUserModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string RoleName { get; set; }
    
    public string? Error { get; set; }
}