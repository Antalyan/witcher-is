using Mapster;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models.Enums;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
using Contract = WitcherProject.DAL.Models.Contract;

namespace WitcherProject.BL.Services.Implementations;

public class ContractService : IContractService
{
    private readonly IUnitOfWorkContracts _contractsUow;
    private readonly KaerMorhenDBContext _context;

    public ContractService(IUnitOfWorkContracts contractsUow, KaerMorhenDBContext context)
    {
        _contractsUow = contractsUow;
        _context = context;
        
        // TODO: setup config if needed
        // TypeAdapterConfig<TSource, TDestination>
        //     .NewConfig()
        //     .Map(dest => dest.FullName,
        //         src => $"{src.FirstName} {src.LastName}");
    }

    public async Task CreateContractAsync(ContractAddDto contractAddDto)
    {
        contractAddDto.State = contractAddDto.PersonId != null ? ContractState.Assigned : ContractState.Created;
        await _contractsUow.ContractRepository.Insert(contractAddDto.Adapt<Contract>());
        await _contractsUow.CommitAsync();
    }

    public async Task<IEnumerable<ContractUpdateDto>> GetAllContractsAsync()
    {
        var returnedContracts = await _contractsUow.ContractRepository.GetAll();
        return returnedContracts.Select(contract => contract.Adapt<ContractUpdateDto>());
    }

    public async Task<IEnumerable<ContractUpdateDto>> GetContractsFiltered(ContractFilterDto contractFilterDto)
        => await new ContractQueryObject(_context).ExecuteQuery(contractFilterDto);

    public async Task<IEnumerable<ContractUpdateDto>> GetContractsByContractorAsync(int contractorId)
    //TODO: set order by, is this method even necessary as filter can be set above?
        => await new ContractQueryObject(_context).ExecuteQuery(new ContractFilterDto {ContractorId = contractorId});

    public async Task<IEnumerable<ContractUpdateDto>> GetContractsAssignedToPersonAsync(int personId)
    //TODO: set order by, is this method even necessary as filter can be set above?
        => await new ContractQueryObject(_context).ExecuteQuery(new ContractFilterDto {PersonId = personId});

    public async Task UpdateContractAsync(ContractUpdateDto contractUpdateDto)
    {
        _contractsUow.ContractRepository.Update(contractUpdateDto.Adapt<Contract>());
        await _contractsUow.CommitAsync();
    }

    //TODO: is this method even necessary as it can be set via update if DTO has corresponding properties?
    public async Task AddPersonToContract(int contractId, int personId)
    {
        var contractToUpdate = await _contractsUow.ContractRepository.GetById(contractId);
        contractToUpdate.PersonId = personId;
        _contractsUow.ContractRepository.Update(contractToUpdate);
        await _contractsUow.CommitAsync();
    }

    //TODO: is this method even necessary as it can be set via update if DTO has corresponding properties?
    public async Task AddContractorToContract(int contractId, int contractorId)
    {
        var contractToUpdate = await _contractsUow.ContractRepository.GetById(contractId);
        contractToUpdate.ContractorId = contractorId;
        _contractsUow.ContractRepository.Update(contractToUpdate);
        await _contractsUow.CommitAsync();
    }

    public async Task DeleteContractAsync(int contractId)
    {
        await _contractsUow.ContractRepository.Delete(contractId);
        await _contractsUow.CommitAsync();
    }
}