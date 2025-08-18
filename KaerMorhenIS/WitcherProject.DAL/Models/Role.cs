using Microsoft.AspNetCore.Identity;

namespace WitcherProject.DAL.Models;

public class Role : IdentityRole<int>
{
    public virtual List<UserRole> UserRoles { get; set; }
    
}