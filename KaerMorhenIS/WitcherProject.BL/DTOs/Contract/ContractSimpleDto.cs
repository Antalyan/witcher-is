using System.ComponentModel.DataAnnotations;

namespace WitcherProject.BL.DTOs.Contract;

public class ContractSimpleDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    protected bool Equals(ContractSimpleDto other)
    {
        return Id == other.Id && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ContractSimpleDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}