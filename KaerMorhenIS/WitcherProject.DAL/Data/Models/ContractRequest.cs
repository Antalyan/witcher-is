using System.ComponentModel.DataAnnotations.Schema;
using WitcherProject.DAL.Data.Models.States;

namespace WitcherProject.DAL.Data.Models;

public class ContractRequest
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? Text { get; set; }

    public ContractRequestState State { get; set; }

    public int PersonId { get; set; }

    [ForeignKey(nameof(PersonId))] public virtual Person Applicant { get; set; }

    public int ContractId { get; set; }

    [ForeignKey(nameof(ContractId))] public virtual Contract Contract { get; set; }
}