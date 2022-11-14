using Microsoft.EntityFrameworkCore;

namespace WitcherProject.DAL.Models;

[Index(nameof(RoleName), IsUnique = true)]
public class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; }
    
    public virtual List<RoleToPerson> RoleToPersons { get; set; }
}