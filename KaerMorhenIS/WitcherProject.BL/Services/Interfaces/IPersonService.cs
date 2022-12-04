using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;

namespace WitcherProject.BL.Services.Interfaces;

public interface IPersonService
{
    Task CreateUserAsync(PersonCreateNewDto personCreateNewDto);

    Task AssignRoleToUserAsync(RoleToPersonDto roleToPersonDto);

    Task UpdateUserAsync(PersonUpdateDto personUpdateDto);

    Task<IEnumerable<PersonCompleteDto>> GetAllUsersAsync();
    
    Task<IEnumerable<PersonSimpleDto>> GetAllSimpleUsersAsync();

    Task<PersonCompleteDto> GetPersonById(int personId);

    Task<PersonCompleteDto> GetPersonById(int personId);

    Task DisableUserByIdAsync(int userId);


}