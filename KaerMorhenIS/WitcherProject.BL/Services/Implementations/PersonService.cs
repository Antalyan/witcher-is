using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    private readonly IRepositoryProvider _repositoryProvider;

    public PersonService(
        IServiceScopeFactory scopeFactory,
        IUnitOfWorkProvider unitOfWorkProvider,
        IRepositoryProvider repositoryProvider)
    {
        _scopeFactory = scopeFactory;
        _unitOfWorkProvider = unitOfWorkProvider;
        _repositoryProvider = repositoryProvider;
    }

    // Wraps each UserManager operation in a fresh scope
    private async Task<TResult> UseUserManagerAsync<TResult>(Func<UserManager<Person>, RoleManager<Role>, Task<TResult>> func)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Person>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        return await func(userManager, roleManager);
    }

    public Task CreateUser(PersonCreateNewDto personCreateNewDto, string password) =>
        UseUserManagerAsync(async (userManager, _) =>
        {
            var newUser = personCreateNewDto.Adapt<Person>();
            var result = await userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
                throw new ApplicationException(ConvertUtil.AggregateErrors(result.Errors));
            return Task.CompletedTask;
        });

    public Task UpdateRoleToUser(string login, List<string> newRoleNames) =>
        UseUserManagerAsync(async (userManager, _) =>
        {
            var user = await userManager.FindByNameAsync(login)
                       ?? throw new ApplicationException("Cannot find user in database");

            var assignedRoles = await userManager.GetRolesAsync(user);
            var rolesToAdd = newRoleNames.Except(assignedRoles).ToList();
            if (rolesToAdd.Any())
            {
                var addResult = await userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                    throw new ApplicationException(ConvertUtil.AggregateErrors(addResult.Errors));
            }

            var rolesToRemove = assignedRoles.Except(newRoleNames).ToList();
            if (rolesToRemove.Any())
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                    throw new ApplicationException(ConvertUtil.AggregateErrors(removeResult.Errors));
            }

            return Task.CompletedTask;
        });

    public Task UpdateUser(PersonUpdateDto personUpdateDto) =>
        UseUserManagerAsync(async (userManager, _) =>
        {
            var user = await userManager.FindByNameAsync(personUpdateDto.UserName)
                       ?? throw new ApplicationException("Cannot find user in database");
            UpdatePerson(user, personUpdateDto);
            await userManager.UpdateAsync(user);
            return Task.CompletedTask;
        });

    public Task<PersonCompleteDto> GetPersonByLogin(string login) =>
        UseUserManagerAsync(async (userManager, _) =>
        {
            var user = await userManager.FindByNameAsync(login);
            return user.Adapt<PersonCompleteDto>();
        });

    public Task<IEnumerable<PersonCompleteDto>> GetAllUserWithRoles() =>
        UseUserManagerAsync((userManager, _) =>
        {
            var users = userManager.Users.Include(u => u.UserRoles)!.ThenInclude(ur => ur.Role).ToList();
            return Task.FromResult(users.Select(u => u.Adapt<PersonCompleteDto>()));
        });

    public Task DisableUserById(int userId) =>
        UseUserManagerAsync(async (userManager, _) =>
        {
            var user = await userManager.FindByIdAsync(userId.ToString())
                       ?? throw new ApplicationException("Cannot find user in database");
            user.IsActive = false;
            await userManager.UpdateAsync(user);
            return Task.CompletedTask;
        });

    public Task<IEnumerable<RoleDto>> GetRoles() =>
        UseUserManagerAsync((_, roleManager) =>
        {
            var roles = roleManager.Roles.ToList();
            return Task.FromResult(roles.Adapt<IEnumerable<RoleDto>>());
        });

    public Task CreateRole(RoleDto roleDto) =>
        UseUserManagerAsync(async (_, roleManager) =>
        {
            var result = await roleManager.CreateAsync(roleDto.Adapt<Role>());
            if (!result.Succeeded)
                throw new ApplicationException(ConvertUtil.AggregateErrors(result.Errors));
            return Task.CompletedTask;
        });

    // Keep UoW-based methods unchanged
    public async Task<IEnumerable<PersonCompleteDto>> GetAllUsers()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var repo = _repositoryProvider.GetRepository<Person>(uow);
        var users = await repo.GetAll();
        return users.Select(u => u.Adapt<PersonCompleteDto>());
    }

    public async Task<IEnumerable<PersonSimpleDto>> GetAllSimpleUsers()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var repo = _repositoryProvider.GetRepository<Person>(uow);
        var users = await repo.GetAll();
        return users.Select(u => u.Adapt<PersonSimpleDto>());
    }

    public async Task<IEnumerable<PersonSimpleDto>> GetAllWitchers()
    {
        var users = await UseUserManagerAsync((userManager, _) =>
            Task.FromResult(userManager.Users
                .Where(u => u.UserRoles.Select(x => x.Role.Name).Contains(RoleNames.Witcher))
                .ToList())
        );
        return users.Select(u => u.Adapt<PersonSimpleDto>());
    }

    public async Task<PersonCompleteDto> GetPersonById(int personId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var repo = _repositoryProvider.GetRepository<Person>(uow);
        var person = await repo.GetById(personId);
        return person.Adapt<PersonCompleteDto>();
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
