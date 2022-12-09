using Microsoft.AspNetCore.Identity;

namespace WitcherProject.BL.DTOs.Person;

public class PersonUpdateDto : IdentityUser<int>
{
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
}