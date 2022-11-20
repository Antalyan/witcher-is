using Mapster;
using Moq;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.BL.Services.Implementations;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
using Xunit;

namespace WitcherProject.BL.Test.PersonTests;

public class PersonServiceTest
{
    private Person _geralt;
    private PersonCompleteDto _geraltCompleteDto;

    private Person _lambert;
    private PersonCompleteDto _lambertCompleteDto;

    private RoleDto _witcherRoleDto;
    private Role _witcherRole;

    public PersonServiceTest()
    {
        _witcherRole = new Role()
        {
            Id = 1,
            RoleName = "Witcher"
        };

        _witcherRoleDto = _witcherRole.Adapt<RoleDto>();

        _geralt = BlTestDataInitalizator.GetPersonDal("Geralt");
        _lambert = BlTestDataInitalizator.GetPersonDal("Lambert");

        var beastOfHonorton = BlTestDataInitalizator.GetContractDal("The Beast of Honorton");
        _geralt.Contracts = new List<Contract> { beastOfHonorton };

        _geraltCompleteDto = BlTestDataInitalizator.GetPersonCompleteDto("Geralt");

        _lambertCompleteDto = BlTestDataInitalizator.GetPersonCompleteDto("Lambert");
    }

    // [Fact]
    // public void PersonCompleteDto_To_Person_MapAll()
    // {
    //     var transformToDto = _geralt.Adapt<PersonCompleteDto>();
    //     var transformToDal = _geraltCompleteDto.Adapt<Person>();
    //     
    //     Assert.Equal(_geralt, transformToDal);
    //     Assert.Equal(transformToDto, _geraltCompleteDto);
    // }

    [Fact]
    public async Task AssigntRoleToUserAsync_RoleIsAssigned()
    {
        var mockPersonUow = new Mock<IUnitOfWorkPersonalData>();

        var geraltWitcherRoleToPerson = new RoleToPerson()
        {
            Role = _witcherRole,
            RoleId = _witcherRole.Id,
            Person = BlTestDataInitalizator.GetPersonSimpleDto("Geralt").Adapt<Person>(),
            PersonId = _geralt.Id,
            ValidFrom = new DateTime(1336, 12, 12),
            ValidTo = new DateTime(1446, 12, 12)
        };
        mockPersonUow.Setup(pow => pow.RoleToPersonRepository.Insert(geraltWitcherRoleToPerson)).Verifiable();

        var personService = new PersonService(mockPersonUow.Object);

        var roleToPersonDto = new RoleToPersonDto()
        {
            Person = BlTestDataInitalizator.GetPersonSimpleDto("Geralt"),
            Role = _witcherRoleDto,
            ValidFrom = new DateTime(1336, 12, 12),
            ValidTo = new DateTime(1446, 12, 12)
        };

        await personService.AssignRoleToUserAsync(roleToPersonDto);

        mockPersonUow.Verify(mow => mow.RoleToPersonRepository.Insert(geraltWitcherRoleToPerson), Times.Once);
    }


    [Fact]
    public async Task DisableUserByIdAsync_Person_IsDisabled()
    {
        var disableLambert = _lambert;
        disableLambert.IsActive = false;
        var mockPersonUow = new Mock<IUnitOfWorkPersonalData>();

        mockPersonUow.Setup(pow => pow.PersonRepository.GetById(2).Result).Returns(_lambert);
        mockPersonUow.Setup(pow => pow.PersonRepository.Update(disableLambert)).Verifiable();

        var personService = new PersonService(mockPersonUow.Object);

        await personService.DisableUserByIdAsync(2);

        mockPersonUow.Verify(mow => mow.PersonRepository.Update(disableLambert), Times.Once);
    }


    [Fact]
    public async Task DisableUserByIdAsync_PersonWrongId_IsNotDisabled()
    {
        var disableLambert = _lambert;
        disableLambert.IsActive = false;
        var mockPersonUow = new Mock<IUnitOfWorkPersonalData>();

        mockPersonUow.Setup(pow => pow.PersonRepository.GetById(2).Result).Returns(_lambert);
        mockPersonUow.Setup(pow => pow.PersonRepository.Update(disableLambert)).Verifiable();

        var personService = new PersonService(mockPersonUow.Object);

        await Assert.ThrowsAsync<NullReferenceException>(() => (personService.DisableUserByIdAsync(1)));
    }

    [Fact]
    public async Task GetAllUserAsync_ReturnEach()
    {
        var mockPersonUow = new Mock<IUnitOfWorkPersonalData>();

        mockPersonUow.Setup(pow => pow.PersonRepository.GetAll().Result)
            .Returns(new List<Person> { _geralt, _lambert });

        var personService = new PersonService(mockPersonUow.Object);

        var result = await personService.GetAllUsersAsync();

        Assert.Equal(_geraltCompleteDto, result.First());
        Assert.Equal(_lambertCompleteDto, result.Last());
    }
}