
using Mapster;
using WitcherProject.BL.DTOs;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;

namespace WitcherProject.BL.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWorkPersonalData _personUow;
    private readonly KaerMorhenDBContext _context;
    public PersonService(IUnitOfWorkPersonalData personUow, KaerMorhenDBContext context)
    {
        _personUow = personUow;
        _context = context;
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

        await _personUow.RoleToPersonRepository.Insert(newUserRole);
        await _personUow.CommitAsync();
    }

    public async Task UpdateUserAsync(PersonUpdateDto personUpdateDto)
    {
        var updatedUser = personUpdateDto.Adapt<Person>();
        
        await _personUow.PersonRepository.Insert(updatedUser);
        await _personUow.CommitAsync();
    }
}