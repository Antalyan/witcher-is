using Microsoft.AspNetCore.Identity;
using WitcherProject.BL.DTOs.Person;

namespace WitcherProject.BL.DTOs;

public class RoleDto : IdentityRole<int>
{
    public virtual List<UserRoleDto> UserRoleDtos { get; set; }
}