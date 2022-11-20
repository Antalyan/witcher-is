using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestFilterDto
{
    public ContractRequestState? State { get; set; }
    
    public int? ContractId { get; set; }
    
    public int? PersonId { get; set; }
    
    public int? RequestedPageNumber { get; set; }

    public bool SortAscending { get; set; }
}