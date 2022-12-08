using Mapster;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

namespace WitcherProject.BL.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    private readonly IGenericRepository<Person> _personRepository;
    
    public PersonService(IUnitOfWorkProvider unitOfWorkProvider, IGenericRepository<Person> personRepository)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _personRepository = personRepository;
    }

    public async Task CreateUserAsync(PersonCreateNewDto personCreateNewDto)
    {
        var newUser = personCreateNewDto.Adapt<Person>();

        await using var uow = _unitOfWorkProvider.CreateUow();
        await _personRepository.Insert(newUser);
        await uow.CommitAsync();
    }

    public async Task AssignRoleToUserAsync(RoleToPersonDto roleToPersonDto)
    {
        //TODO: update after authentication
        var newUserRole = roleToPersonDto.Adapt<UserRole>();    
        
        await using var uow = _unitOfWorkProvider.CreateUow();
        // await personUow.RoleToPersonRepository.Insert(newUserRole); // todo - will this work since the mapper most probably won't create Role Role and Person Person properties of the RoleToPerson class?
        await uow.CommitAsync();
    }

    public async Task UpdateUserAsync(PersonUpdateDto personUpdateDto)
    {
        var updatedUser = personUpdateDto.Adapt<Person>();
        await using var uow = _unitOfWorkProvider.CreateUow();
        _personRepository.Update(updatedUser);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<PersonCompleteDto>> GetAllUsersAsync()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedPersons = await _personRepository.GetAll();
        return returnedPersons.Select(person => person.Adapt<PersonCompleteDto>());
    }
    
    public async Task<IEnumerable<PersonSimpleDto>> GetAllSimpleUsersAsync()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedPersons = await _personRepository.GetAll();
        return returnedPersons.Select(person => person.Adapt<PersonSimpleDto>());
    }

    public async Task<PersonCompleteDto> GetPersonById(int personId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedPerson = await _personRepository.GetById(personId);
        return returnedPerson.Adapt<PersonCompleteDto>();
    }

    public async Task DisableUserByIdAsync(int userId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var userToDisable = await _personRepository.GetById(userId);
        userToDisable.IsActive = false;
        _personRepository.Update(userToDisable);
        await uow.CommitAsync();
    }
}