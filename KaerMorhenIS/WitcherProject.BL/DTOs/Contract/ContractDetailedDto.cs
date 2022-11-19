using WitcherProject.BL.DTOs.Contractor;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.Contract;

public class ContractDetailedDto
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public ContractState? State { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }

    public ContractorDto? Contractor { get; set; }
    
    public PersonSimpleDto? Person { get; set; }
    
    public virtual List<ContractRequestSimpleDto> ContractRequests { get; set; }
}