namespace WitcherProject.BL.DTOs;

public class ContractGetDto
{
    // TODO

    public string Name { get; set; }

    public string? Description { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }
    
    public Contractor? Contractor { get; set; }

    public Person? Person { get; set; }
}