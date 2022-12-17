using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;

namespace WitcherProject.BL.Services.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonCompleteDto>> GetAllUsers();
    
    Task<IEnumerable<PersonSimpleDto>> GetAllSimpleUsers();

    Task<IEnumerable<PersonCompleteDto>> GetAllUserWithRoles();

    Task<PersonCompleteDto> GetPersonById(int personId);

    Task<PersonCompleteDto> GetPersonByLogin(string login);
    
    Task CreateUser(PersonCreateNewDto personCreateNewDto, string password);

    Task UpdateRoleToUser(string login, List<string> newRoleNames);

    Task UpdateUser(PersonUpdateDto personUpdateDto);

    Task DisableUserById(int userId);
    
    Task<IEnumerable<RoleDto>> GetRoles();

    Task CreateRole(RoleDto roleDto);

}