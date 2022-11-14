using System.ComponentModel.DataAnnotations.Schema;
using WitcherProject.DAL.Models.Enums;

namespace WitcherProject.DAL.Models;

public class Contract
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public ContractState? State { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Location { get; set; }

    public int? ContractorId { get; set; }

    [ForeignKey(nameof(ContractorId))] public virtual Contractor? Contractor { get; set; }

    public int? PersonId { get; set; }
    [ForeignKey(nameof(PersonId))] public virtual Person? Person { get; set; }
    
    public virtual List<ContractRequest> ContractRequests { get; set; }
}