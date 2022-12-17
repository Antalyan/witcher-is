using Microsoft.AspNetCore.Identity;

namespace WitcherProject.BL.DTOs.Person;

public class UserRoleDto : IdentityUserRole<int>
{
    public virtual RoleDto Role { get; set; }
    
    public virtual PersonSimpleDto User { get; set; } 
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}