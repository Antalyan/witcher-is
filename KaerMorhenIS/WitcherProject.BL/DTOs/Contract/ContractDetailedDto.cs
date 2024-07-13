using System.ComponentModel.DataAnnotations;
using WitcherProject.BL.DTOs.Contractor;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.Contract;

public class ContractDetailedDto
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public ContractState State { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }

    public ContractorDto? Contractor { get; set; }
    
    public PersonSimpleDto? Person { get; set; }

    public virtual List<ContractRequestSimpleDto> ContractRequests { get; set; }

    protected bool Equals(ContractDetailedDto other)
    {
        return Id == other.Id && Name == other.Name && Description == other.Description && State == other.State && Nullable.Equals(StartDate, other.StartDate) && Nullable.Equals(EndDate, other.EndDate) && Nullable.Equals(Deadline, other.Deadline) && Location == other.Location && Nullable.Equals(Contractor, other.Contractor) && Nullable.Equals(Person, other.Person) && Nullable.Equals(ContractRequests,other.ContractRequests);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ContractDetailedDto)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(Name);
        hashCode.Add(Description);
        hashCode.Add(State);
        hashCode.Add(StartDate);
        hashCode.Add(EndDate);
        hashCode.Add(Deadline);
        hashCode.Add(Location);
        hashCode.Add(Contractor);
        hashCode.Add(Person);
        hashCode.Add(ContractRequests);
        return hashCode.ToHashCode();
    }
}