using Microsoft.AspNetCore.Identity;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;

namespace WitcherProject.BL.DTOs.Person;

public class PersonCompleteDto : IdentityUser<int>
{
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
    public virtual List<ContractSimpleDto> Contracts { get; set; }
    public virtual List<UserRoleDto> UserRoleDtos { get; set; }
    public virtual List<ContractRequestDetailedDto> ContractRequests { get; set; }
    
}