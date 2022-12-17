using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WitcherProject.DAL.Models;

public class Role : IdentityRole<int>
{
    public virtual List<UserRole> UserRoles { get; set; }
    
}