using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestSimpleDto
{
    public int? Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public ContractRequestState? State { get; set; }
}