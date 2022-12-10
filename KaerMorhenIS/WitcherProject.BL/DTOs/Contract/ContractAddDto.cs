using System.ComponentModel.DataAnnotations;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.Contract;

public class ContractAddDto
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public ContractState? State { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }

    public int? ContractorId { get; set; }

    public int? PersonId { get; set; }
}