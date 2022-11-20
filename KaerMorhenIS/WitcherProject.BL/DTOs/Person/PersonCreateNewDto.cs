using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;

namespace WitcherProject.BL.DTOs.Person;

public class PersonCreateNewDto
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }    // todo - change to  plain psswd that will be later hashed - update when authentication is implemented
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? Cv { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsActive { get; set; }
    public virtual List<RoleToPersonDto> RoleToPersons { get; set; }
}