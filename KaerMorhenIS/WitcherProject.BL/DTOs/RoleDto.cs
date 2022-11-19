using WitcherProject.BL.DTOs.Person;

namespace WitcherProject.BL.DTOs;

public class RoleDto
{
    public int Id { get; set; }

    public string RoleName { get; set; }
    
    public virtual List<RoleToPersonDto> RoleToPersonDtos { get; set; }
}