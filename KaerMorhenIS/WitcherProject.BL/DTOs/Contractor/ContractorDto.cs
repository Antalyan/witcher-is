namespace WitcherProject.BL.DTOs.Contractor;

public class ContractorDto
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public string? Surname { get; set; }

    // public virtual List<Contract> Contracts { get; set; } TODO: add contractDto list or id list or nothing?

    protected bool Equals(ContractorDto other)
    {
        return Id == other.Id && Name == other.Name && Surname == other.Surname;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ContractorDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Surname);
    }
}