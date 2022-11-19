namespace WitcherProject.DAL.Models;

public class Contractor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Surname { get; set; }

    public virtual List<Contract> Contracts { get; set; }

    protected bool Equals(Contractor other)
    {
        return Id == other.Id && Name == other.Name && Surname == other.Surname && Nullable.Equals(Contracts, other.Contracts);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Contractor)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Surname, Contracts);
    }
}