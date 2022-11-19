using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;

namespace WitcherProject.BL.DTOs.Person;

public class PersonCompleteDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
    public virtual List<ContractSimpleDto> Contracts { get; set; }
    public virtual List<RoleToPersonDto> RoleToPersons { get; set; }
    public virtual List<ContractRequestDetailedDto> ContractRequests { get; set; }

    protected bool Equals(PersonCompleteDto other)
    {
        var simpleArgsEqual = Id == other.Id && Login == other.Login && PasswordHash == other.PasswordHash &&
                     Name == other.Name && Surname == other.Surname &&
                     Cv == other.Cv && Birthdate.Equals(other.Birthdate) && IsActive == other.IsActive;
        var contractsEqual = Contracts is null ? other.Contracts is null : Contracts.SequenceEqual(other.Contracts);
        var roleToPersonsEqual = RoleToPersons is null
            ? other.RoleToPersons is null
            : RoleToPersons.SequenceEqual(other.RoleToPersons);
        var contractRequestsEqual = RoleToPersons is null
            ? other.RoleToPersons is null
            : RoleToPersons.SequenceEqual(other.RoleToPersons);
        return simpleArgsEqual && contractsEqual && roleToPersonsEqual && contractRequestsEqual;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PersonCompleteDto)obj);
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