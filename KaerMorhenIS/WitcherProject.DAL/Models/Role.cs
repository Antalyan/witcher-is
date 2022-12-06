using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WitcherProject.DAL.Models;

[Index(nameof(RoleName), IsUnique = true)]
public class Role : IdentityRole<int>
{
    public int Id { get; set; }

    public string RoleName { get; set; }
    
    public virtual List<RoleToPerson> RoleToPersons { get; set; }

    protected bool Equals(Role other)
    {
        return Id == other.Id && RoleName == other.RoleName && Nullable.Equals(RoleToPersons, other.RoleToPersons);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Role)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, RoleName, RoleToPersons);
    }
}