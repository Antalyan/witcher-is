using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestDetailedDto
{
    public int? Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Text { get; set; }

    public ContractRequestState? State { get; set; }

    public PersonSimpleDto Applicant { get; set; }

    public ContractSimpleDto Contract { get; set; }
}