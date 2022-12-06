using Microsoft.Build.Framework;

namespace WitcherProject.PresentationLayer.Model;

public class RoleUserModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string RoleName { get; set; }
}