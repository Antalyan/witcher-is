using System.ComponentModel.DataAnnotations.Schema;

namespace WitcherProject.DAL.Models;

public class RoleToPerson
{
    public int Id { get; set; }
    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))] public virtual Role Role { get; set; }
    public int PersonId { get; set; }

    [ForeignKey(nameof(PersonId))] public virtual Person Person { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    protected bool Equals(RoleToPerson other)
    {
        return Id == other.Id && RoleId == other.RoleId && Role.Equals(other.Role) && PersonId == other.PersonId && Person.Equals(other.Person) && ValidFrom.Equals(other.ValidFrom) && Nullable.Equals(ValidTo, other.ValidTo);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RoleToPerson)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, RoleId, Role, PersonId, Person, ValidFrom, ValidTo);
    }
}