using System.ComponentModel.DataAnnotations;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.DTOs.Contract;

public class ContractUpdateDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public ContractState State { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }

    public int? ContractorId { get; set; }
    
    public int? PersonId { get; set; }
}