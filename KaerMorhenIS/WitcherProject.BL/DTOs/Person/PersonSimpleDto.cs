namespace WitcherProject.BL.DTOs.Person;

public class PersonSimpleDto 
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }

    protected bool Equals(PersonSimpleDto other)
    {
        return Id == other.Id && Login == other.Login && Name == other.Name && Surname == other.Surname;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PersonSimpleDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Login, Name, Surname);
    }
}