namespace WitcherProject.BL.DTOs.Person;

public class RoleToPersonDto
{
    public int Id { get; set; }
    public RoleDto Role { get; set; }
    
    public PersonSimpleDto Person { get; set; } 
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}