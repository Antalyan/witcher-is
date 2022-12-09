using System.ComponentModel.DataAnnotations;

namespace WitcherProject.PresentationLayer.Model.Administration;

public class RoleUserModel : BaseModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string RoleName { get; set; }
    
}