using WitcherProject.BL.DTOs;

namespace WitcherProject.BL.Services;

public interface IPersonService
{
    Task CreateUserAsync(PersonCreateNewDto personCreateNewDto);

    Task AssignRoleToUserAsync(RoleToPersonDto roleToPersonDto);

    Task UpdateUserAsync(PersonUpdateDto personUpdateDto);
}