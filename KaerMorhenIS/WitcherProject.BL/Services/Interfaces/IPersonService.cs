using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;

namespace WitcherProject.BL.Services.Interfaces;

public interface IPersonService
{
    Task CreateUserAsync(PersonCreateNewDto personCreateNewDto, string password);

    Task AssignRoleToUserAsync(string login, string roleName);

    Task UpdateUserAsync(PersonUpdateDto personUpdateDto);

    Task<IEnumerable<PersonCompleteDto>> GetAllUsersAsync();
    
    Task<IEnumerable<PersonSimpleDto>> GetAllSimpleUsersAsync();

    Task<IEnumerable<PersonCompleteDto>> GetAllUserWithRoles();

    Task<PersonCompleteDto> GetPersonById(int personId);

    Task<PersonCompleteDto> GetPersonByLogin(string login);

    Task DisableUserByIdAsync(int userId);
    
    Task<IEnumerable<RoleDto>> GetRoles();

    Task CreateRole(RoleDto roleDto);

}