using System.ComponentModel.DataAnnotations.Schema;
using WitcherProject.DAL.Models;
using WitcherProject.DAL.Models.Enums;

namespace WitcherProject.BL.DTOs.ContractRequest;

public class ContractRequestDto
{
    public int? Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Text { get; set; }

    public ContractRequestState? State { get; set; }

    public int? PersonId { get; set; }

    public int? ContractId { get; set; }
}