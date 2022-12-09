using Microsoft.AspNetCore.Identity;

namespace WitcherProject.BL.DTOs.Person;

public class PersonCreateNewDto : IdentityUser<int>
{
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
    public virtual List<UserRoleDto> RoleToPersons { get; set; }
}