using System.ComponentModel.DataAnnotations.Schema;

namespace WitcherProject.DAL.Models;

public class RoleToPerson
{
    public int Id { get; set; }
    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))] public virtual Role AssignedRole { get; set; }
    public int PersonId { get; set; }

    [ForeignKey(nameof(PersonId))] public virtual Person Person { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }
}