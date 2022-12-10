using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared;

namespace WitcherProject.BL.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    private readonly IGenericRepository<Person> _personRepository;
    private readonly UserManager<Person> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public PersonService(IUnitOfWorkProvider unitOfWorkProvider, IGenericRepository<Person> personRepository, UserManager<Person> userManager,
        RoleManager<Role> roleManager)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _personRepository = personRepository;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task CreateUserAsync(PersonCreateNewDto personCreateNewDto, string password)
    {
        var newUser = personCreateNewDto.Adapt<Person>();

        await using var uow = _unitOfWorkProvider.CreateUow();
        var callResult = await _userManager.CreateAsync(newUser, password);
        if (!callResult.Succeeded)
            throw new ApplicationException(ConvertUtil.AggregateErrors(callResult.Errors));
        await uow.CommitAsync();
    }

    
    public async Task AssignRoleToUserAsync(string login, string roleName)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var userRoleAssignedTo = await _userManager.FindByNameAsync(login);
        if (userRoleAssignedTo is null)
        {
            throw new ApplicationException("Cannot find user in database");
        }
        var assignResult = await _userManager.AddToRoleAsync(userRoleAssignedTo, roleName);
        if (!assignResult.Succeeded) throw new ApplicationException(ConvertUtil.AggregateErrors(assignResult.Errors));
        await uow.CommitAsync();
    }

    public async Task UpdateUserAsync(PersonUpdateDto personUpdateDto)
    {
        var updatedPerson = await _userManager.FindByNameAsync(personUpdateDto.UserName);
        await using var uow = _unitOfWorkProvider.CreateUow();
        UpdatePerson(updatedPerson, personUpdateDto);
        await _userManager.UpdateAsync(updatedPerson);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<PersonCompleteDto>> GetAllUsersAsync()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedPersons = await _personRepository.GetAll();
        await uow.CommitAsync();
        return returnedPersons.Select(person => person.Adapt<PersonCompleteDto>());
    }
    
    public async Task<IEnumerable<PersonCompleteDto>> GetAllUserWithRoles()
    {
        var returnedPersons = _userManager.Users.Include(u => u.UserRoles)!.ThenInclude(ur => ur.Role);
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
    
    public async Task<PersonCompleteDto> GetPersonByLogin(string login)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedPerson = await _userManager.FindByNameAsync(login);
        return returnedPerson.Adapt<PersonCompleteDto>();
    }


    public async Task DisableUserByIdAsync(int userId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var userToDisable = await _userManager.FindByIdAsync(userId.ToString());
        userToDisable.IsActive = false;
        await _userManager.UpdateAsync(userToDisable);
        await uow.CommitAsync();
    }

    public async Task<List<RoleDto>> GetRoles()
    {
        var inter = _roleManager.Roles.ToList();
       return inter.Adapt<List<RoleDto>>();
    }

    public async Task CreateRole(RoleDto roleDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var createResult = await _roleManager.CreateAsync(roleDto.Adapt<Role>());
        if(!createResult.Succeeded) throw new ApplicationException(ConvertUtil.AggregateErrors(createResult.Errors));
        await uow.CommitAsync();
    }

    private void UpdatePerson(Person update, PersonUpdateDto toUpdate)
    {
        update.Name = toUpdate.Name;
        update.Surname = toUpdate.Surname;
        update.Cv = toUpdate.Cv;
        update.Birthdate = toUpdate.Birthdate;
        update.IsActive = toUpdate.IsActive;
    }
    
}