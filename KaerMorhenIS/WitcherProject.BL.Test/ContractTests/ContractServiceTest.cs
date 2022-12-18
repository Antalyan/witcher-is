using Mapster;
using Moq;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Implementations;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;
using Xunit;

namespace WitcherProject.BL.Test.ContractTests;

public class ContractServiceTest
{
    protected Contract _devilByTheWell;
    protected Contract _jennyOTheWoods;
    protected Contract _beastOfHonorton;
    protected List<Contract> _allContracts;
    
    protected ContractDetailedDto _devilByTheWellDetailedDto;
    protected ContractDetailedDto _jennyOTheWoodsDetailedDto;
    protected ContractDetailedDto _beastOfHonortonDetailedDto;
    
    public ContractServiceTest()
    {
        _devilByTheWell = BlTestDataInitalizator.GetContractDal("Devil by the Well");
        _jennyOTheWoods = BlTestDataInitalizator.GetContractDal("Jenny o' the Woods");
        _beastOfHonorton = BlTestDataInitalizator.GetContractDal("The Beast of Honorton");

        _allContracts = new List<Contract>
        {
            _devilByTheWell, _jennyOTheWoods, _beastOfHonorton
        };
        // DTOs
        
        _devilByTheWellDetailedDto = BlTestDataInitalizator.GetContractDetailedDto("Devil by the Well");
        _jennyOTheWoodsDetailedDto = BlTestDataInitalizator.GetContractDetailedDto("Jenny o' the Woods");
        _beastOfHonortonDetailedDto = BlTestDataInitalizator.GetContractDetailedDto("The Beast of Honorton");
    }
    
    [Fact]
    public async Task GetAllContracts_ReturnAllContracts()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();

        mockContractRepository.Setup(repo => repo.GetAll().Result)
            .Returns(_allContracts);

        var contractService = new ContractService(mockUnitOfWorkProvider.Object,
            mockQueryObject.Object, mockContractRepository.Object);
        var result = await contractService.GetAllContracts();
        
        Assert.Equal(3, result.Count());
    }
    
    [Fact]
    public async Task CreateContract_Valid_CallsRepositoryInsert()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var repositoryContracts = new List<Contract>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();

        var contractAddDto = new ContractUpsertDto()
        {
            ContractorId = 1,
            Name = "Devil by the Well",
            Description = "Slay the bitch - Odolan",
            State = ContractState.Open,
            StartDate = new DateTime(2022, 8, 8),
            EndDate = new DateTime(2022, 10, 1),
            Deadline = new DateTime(2022, 11, 1),
            Location = "White Orchard",
        };

        var mockUoW = new EFUnitOfWork(new Mock<KaerMorhenDBContext>().Object);
        mockUnitOfWorkProvider.Setup(provider => provider.CreateUow()).Returns(mockUoW);
        mockContractRepository.Setup(repo => repo.Insert(It.IsAny<Contract>()))
            .Callback(() => repositoryContracts.Add(contractAddDto.Adapt<Contract>()));
        var contractFacade = new ContractFacade(mockUnitOfWorkProvider.Object, 
            new ContractService(mockUnitOfWorkProvider.Object, new Mock<IContractQueryObject>().Object, mockContractRepository.Object),
            new ContractRequestService(mockUnitOfWorkProvider.Object, new Mock<IContractRequestQueryObject>().Object, new Mock<IGenericRepository<ContractRequest>>().Object),
            new ContractorService(mockUnitOfWorkProvider.Object, new Mock<IGenericRepository<Contractor>>().Object));
       
        await contractFacade.SaveContract(null, 1, contractAddDto);
        
        Assert.Single(repositoryContracts);
    }

    [Fact]
    public async Task UpdateContract_Valid_CallsUpdate()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();

        var mockUoW = new EFUnitOfWork(new Mock<KaerMorhenDBContext>().Object);
        mockUnitOfWorkProvider.Setup(provider => provider.CreateUow()).Returns(mockUoW);
        mockContractRepository.Setup(repo => repo.Update(It.IsAny<Contract>()))
            .Verifiable();
        
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object);
        var contractUpdateDto = new ContractUpsertDto
        {
            ContractorId = 1,
            Name = "Devil by the Well",
            Description = "Slay the bitch - Odolan",
            State = ContractState.Open,
            StartDate = new DateTime(2022, 8, 8),
            EndDate = new DateTime(2022, 10, 1),
            Deadline = new DateTime(2022, 11, 1),
            Location = "White Orchard",
        };
        
        contractService.UpdateContract(contractUpdateDto);
        
        mockContractRepository.Verify(repo => repo.Update(It.IsAny<Contract>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteContract_Deletes()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();

        var contractForDelete = _jennyOTheWoods;

        var mockUoW = new EFUnitOfWork(new Mock<KaerMorhenDBContext>().Object);
        mockUnitOfWorkProvider.Setup(provider => provider.CreateUow()).Returns(mockUoW);
        mockContractRepository.Setup(repo => repo.Delete(_jennyOTheWoods.Id))
            .Callback(() => contractForDelete = null);
        
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object);
        
        await contractService.DeleteContract(_jennyOTheWoods.Id);
        
        Assert.Null(contractForDelete);
    }

    [Fact]
    public async Task GetContractsFiltered_Returns_Exact()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();

        mockQueryObject.Setup(mqo => mqo.ExecuteQuery(It.IsAny<ContractFilterDto>()).Result)
            .Returns(new List<ContractDetailedDto>{_devilByTheWellDetailedDto, _jennyOTheWoodsDetailedDto});
        
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object);

        var result = await contractService.GetContractsFiltered(new ContractFilterDto() { ContractorId = 1});

        Assert.Equal(_devilByTheWellDetailedDto, result.First());
    }
    
}