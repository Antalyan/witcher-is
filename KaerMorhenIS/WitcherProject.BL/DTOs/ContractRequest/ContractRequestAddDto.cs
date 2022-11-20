using System.ComponentModel.DataAnnotations.Schema;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestAddDto
{
    public string? Text { get; set; }

    public ContractRequestState State { get; set; }

    public int PersonId { get; set; }
    
    public int ContractId { get; set; }
}