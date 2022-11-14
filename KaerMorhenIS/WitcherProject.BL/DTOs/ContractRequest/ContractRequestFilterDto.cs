using WitcherProject.DAL.Models.Enums;

namespace WitcherProject.BL.DTOs.Contract;

public class ContractRequestFilterDto
{
    public ContractRequestState? State { get; set; }
    
    public int? ContractId { get; set; }
    
    public int? PersonId { get; set; }
    
    public int? RequestedPageNumber { get; set; }
    
    public string? SortCriteria { get; set; }
    
    public bool SortAscending { get; set; }
}