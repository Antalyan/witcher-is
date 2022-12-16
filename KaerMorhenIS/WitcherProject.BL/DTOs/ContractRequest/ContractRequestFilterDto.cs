using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestFilterDto
{
    public int? Id { get; set; }

    public ContractRequestState? State { get; set; }
    
    public int? ContractId { get; set; }
    
    public int? PersonId { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public int? RequestedPageNumber { get; set; }

    public bool SortAscending { get; set; }
}