using Microsoft.EntityFrameworkCore;

namespace WitcherProject.DAL.Models;

[Index(nameof(Login), IsUnique = true)]
public class Person
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }

    public virtual List<Contract> Contracts { get; set; }
    
    public virtual List<RoleToPerson> RoleToPersons { get; set; }
    
    public virtual List<ContractRequest>? ContractRequests { get; set; }

    protected bool Equals(Person other)
    {
        return Id == other.Id && Login == other.Login && PasswordHash == other.PasswordHash && Name == other.Name && Surname == other.Surname && Cv == other.Cv && Birthdate.Equals(other.Birthdate) && IsActive == other.IsActive && Nullable.Equals(Contracts, other.Contracts) && Nullable.Equals(RoleToPersons, other.RoleToPersons) && Nullable.Equals(ContractRequests, other.ContractRequests);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Person)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(Login);
        hashCode.Add(PasswordHash);
        hashCode.Add(Name);
        hashCode.Add(Surname);
        hashCode.Add(Cv);
        hashCode.Add(Birthdate);
        hashCode.Add(IsActive);
        hashCode.Add(Contracts);
        hashCode.Add(RoleToPersons);
        hashCode.Add(ContractRequests);
        return hashCode.ToHashCode();
    }
}