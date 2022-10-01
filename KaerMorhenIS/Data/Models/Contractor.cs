namespace WitcherProject.Data.Models;

public class Contractor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public virtual List<Contract> Contracts { get; set; }
}