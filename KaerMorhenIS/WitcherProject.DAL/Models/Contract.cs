using System.ComponentModel.DataAnnotations.Schema;
using WitcherProject.Shared.Enums;

namespace WitcherProject.DAL.Models;

public class Contract
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public ContractState? State { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }

    public int? ContractorId { get; set; }

    [ForeignKey(nameof(ContractorId))] public virtual Contractor? Contractor { get; set; }

    public int? PersonId { get; set; }
    [ForeignKey(nameof(PersonId))] public virtual Person? Person { get; set; }
    
    public virtual List<ContractRequest> ContractRequests { get; set; }


    protected bool Equals(Contract other)
    {
        return Id == other.Id && Name == other.Name && Description == other.Description && State == other.State && Nullable.Equals(StartDate, other.StartDate) && Nullable.Equals(EndDate, other.EndDate) && Nullable.Equals(Deadline, other.Deadline) && Location == other.Location && ContractorId == other.ContractorId && Nullable.Equals(Contractor, other.Contractor) && PersonId == other.PersonId && Nullable.Equals(Person, other.Person) && Nullable.Equals(ContractRequests, other.ContractRequests);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Contract)obj);
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
        hashCode.Add(ContractorId);
        hashCode.Add(Contractor);
        hashCode.Add(PersonId);
        hashCode.Add(Person);
        hashCode.Add(ContractRequests);
        return hashCode.ToHashCode();
    }
}