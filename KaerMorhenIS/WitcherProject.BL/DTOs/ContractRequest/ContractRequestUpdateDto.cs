using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestUpdateDto
{
    public int? Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Text { get; set; }

    public ContractRequestState? State { get; set; }

    public int PersonId { get; set; }
    
    public int ContractId { get; set; }
}