using Mapster;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;

namespace WitcherProject.BL.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IUnitOfWorkPersonalData _personUow;
    public PersonService(IUnitOfWorkPersonalData personUow)
    {
        _personUow = personUow;
    }

    public async Task CreateUserAsync(PersonCreateNewDto personCreateNewDto)
    {
        var newUser = personCreateNewDto.Adapt<Person>();

        await _personUow.PersonRepository.Insert(newUser);
        await _personUow.CommitAsync();
    }

    public async Task AssignRoleToUserAsync(RoleToPersonDto roleToPersonDto)
    {
        var newUserRole = roleToPersonDto.Adapt<RoleToPerson>();    

        await _personUow.RoleToPersonRepository.Insert(newUserRole); // todo - will this work since the mapper most probably won't create Role Role and Person Person properties of the RoleToPerson class?
        await _personUow.CommitAsync();
    }

    public async Task UpdateUserAsync(PersonUpdateDto personUpdateDto)
    {
        var updatedUser = personUpdateDto.Adapt<Person>();
        _personUow.PersonRepository.Update(updatedUser);
        await _personUow.CommitAsync();
    }

    public async Task<IEnumerable<PersonCompleteDto>> GetAllUsersAsync()
    {
        var returnedPersons = await _personUow.PersonRepository.GetAll();
        return returnedPersons.Select(person => person.Adapt<PersonCompleteDto>());
    }
    
    public async Task<IEnumerable<PersonSimpleDto>> GetAllSimpleUsersAsync()
    {
        var returnedPersons = await _personUow.PersonRepository.GetAll();
        return returnedPersons.Select(person => person.Adapt<PersonSimpleDto>());
    }

    public async Task<PersonCompleteDto> GetPersonById(int personId)
    {
        var returnedPerson = await _personUow.PersonRepository.GetById(personId);
        return returnedPerson.Adapt<PersonCompleteDto>();
    }

    public async Task<PersonCompleteDto> GetPersonById(int personId)
    {
        var returnedPerson = await _personUow.PersonRepository.GetById(personId);
        return returnedPerson.Adapt<PersonCompleteDto>();
    }

    public async Task DisableUserByIdAsync(int userId)
    {
        var userToDisable = await _personUow.PersonRepository.GetById(userId);
        userToDisable.IsActive = false;
        _personUow.PersonRepository.Update(userToDisable);   
    }
}