
namespace WitcherProject.PresentationLayer.Model.Administration;

public class RoleUserModel : BaseModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
    public virtual IEnumerable<String> RoleNames { get; set; }

}