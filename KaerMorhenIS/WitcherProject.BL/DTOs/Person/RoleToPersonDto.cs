namespace WitcherProject.BL.DTOs;

public class RoleToPersonDto
{
    public int RoleId { get; set; }
    public int PersonId { get; set; } 
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}