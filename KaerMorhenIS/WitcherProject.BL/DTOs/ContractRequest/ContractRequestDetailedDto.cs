using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestDetailedDto
{
    public int? Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Text { get; set; }

    public ContractRequestState? State { get; set; }

    public PersonSimpleDto Applicant { get; set; }

    public ContractSimpleDto Contract { get; set; }

    protected bool Equals(ContractRequestDetailedDto other)
    {
        return Id == other.Id && Nullable.Equals(CreatedOn, other.CreatedOn) && Text == other.Text && State == other.State && Applicant.Equals(other.Applicant) && Contract.Equals(other.Contract);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ContractRequestDetailedDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CreatedOn, Text, State, Applicant, Contract);
    }
}