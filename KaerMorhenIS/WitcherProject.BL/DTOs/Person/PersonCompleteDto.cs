using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;

namespace WitcherProject.BL.DTOs.Person;

public class PersonCompleteDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
    public virtual List<ContractSimpleDto> Contracts { get; set; }
    public virtual List<RoleToPersonDto> RoleToPersons { get; set; }
    public virtual List<ContractRequestDetailedDto> ContractRequests { get; set; }
}