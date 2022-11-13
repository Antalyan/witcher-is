using WitcherProject.DAL.Models.Enums;

namespace WitcherProject.BL.DTOs.Contract;

public class ContractDto
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public ContractState? State { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }

    public int? ContractorId { get; set; }
    
    public int? PersonId { get; set; }
}