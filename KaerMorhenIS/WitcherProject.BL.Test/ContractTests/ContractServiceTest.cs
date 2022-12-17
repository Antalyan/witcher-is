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

    protected List<ContractDetailedDto> _allContractDetailedDtos;
    
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
        _allContractDetailedDtos = new List<ContractDetailedDto>
        {
            _devilByTheWellDetailedDto, _jennyOTheWoodsDetailedDto, _beastOfHonortonDetailedDto
        };
    }
    
    [Fact]
    public async Task GetAllContractsAsync_ReturnAllContracts()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();
        var mockContractorRepository = new Mock<IGenericRepository<Contractor>>();
        var mockContractRequestRepository = new Mock<IGenericRepository<ContractRequest>>();

        mockContractRepository.Setup(repo => repo.GetAll().Result)
            .Returns(_allContracts);
        
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object, 
            mockContractorRepository.Object, mockContractRequestRepository.Object);
        var result = await contractService.GetAllContractsAsync();
        
        Assert.Equal(3, result.Count());
    }
    
    [Fact]
    public async Task CreateContractAsync_Valid_CallsRepositoryInsert()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();
        var mockContractorRepository = new Mock<IGenericRepository<Contractor>>();
        var mockContractRequestRepository = new Mock<IGenericRepository<ContractRequest>>();

        var repositoryContracts = new List<Contract>();

        var contractAddDto = new ContractAddDto()
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
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object, 
            mockContractorRepository.Object, mockContractRequestRepository.Object);
       
        await contractService.CreateContractAsync(contractAddDto);
        
        Assert.Single(repositoryContracts);
    }

    [Fact]
    public async Task UpdateContractAsync_Valid_CallsUpdate()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();
        var mockContractorRepository = new Mock<IGenericRepository<Contractor>>();
        var mockContractRequestRepository = new Mock<IGenericRepository<ContractRequest>>();

        var mockUoW = new EFUnitOfWork(new Mock<KaerMorhenDBContext>().Object);
        mockUnitOfWorkProvider.Setup(provider => provider.CreateUow()).Returns(mockUoW);
        mockContractRepository.Setup(repo => repo.Update(It.IsAny<Contract>()))
            .Verifiable();
        
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object, 
            mockContractorRepository.Object, mockContractRequestRepository.Object);
        var contractUpdateDto = new ContractUpdateDto
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
        
        await contractService.UpdateContractAsync(contractUpdateDto);
        
        mockContractRepository.Verify(repo => repo.Update(It.IsAny<Contract>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteContractAsync_Deletes()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();
        var mockContractorRepository = new Mock<IGenericRepository<Contractor>>();
        var mockContractRequestRepository = new Mock<IGenericRepository<ContractRequest>>();

        var contractForDelete = _jennyOTheWoods;

        var mockUoW = new EFUnitOfWork(new Mock<KaerMorhenDBContext>().Object);
        mockUnitOfWorkProvider.Setup(provider => provider.CreateUow()).Returns(mockUoW);
        mockContractRepository.Setup(repo => repo.Delete(_jennyOTheWoods.Id))
            .Callback(() => contractForDelete = null);
        
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object, 
            mockContractorRepository.Object, mockContractRequestRepository.Object);

        
        await contractService.DeleteContractAsync(_jennyOTheWoods.Id);
        
        Assert.Null(contractForDelete);
    }
    //
    // [Fact]
    // public void ContractDetailedDto_To_Contract_MapAll()
    // {
    //
    //     var transformToDto = _beastOfHonorton.Adapt<ContractDetailedDto>();
    //     var transformToDal = _beastOfHonortonDetailedDto.Adapt<Contract>();
    //
    //     Assert.Equal(_beastOfHonorton, transformToDal);
    //     Assert.Equal(transformToDto, _beastOfHonortonDetailedDto);
    //     
    // }
    
    [Fact]
    public async Task GetContractsFilteredAsync_Returns_Exact()
    {
        var mockUnitOfWorkProvider = new Mock<IUnitOfWorkProvider>();
        var mockQueryObject = new Mock<IContractQueryObject>();
        var mockContractRepository = new Mock<IGenericRepository<Contract>>();
        var mockContractorRepository = new Mock<IGenericRepository<Contractor>>();
        var mockContractRequestRepository = new Mock<IGenericRepository<ContractRequest>>();
        
        mockQueryObject.Setup(mqo => mqo.ExecuteQuery(It.IsAny<ContractFilterDto>()).Result)
            .Returns(new List<ContractDetailedDto>{_devilByTheWellDetailedDto, _jennyOTheWoodsDetailedDto});
        
        var contractService = new ContractService(mockUnitOfWorkProvider.Object, 
            mockQueryObject.Object, mockContractRepository.Object, 
            mockContractorRepository.Object, mockContractRequestRepository.Object);

        var result = await contractService.GetContractsFilteredAsync(new ContractFilterDto() { ContractorId = 1});

        Assert.Equal(_devilByTheWellDetailedDto, result.First());
    }
    
}