using Mapster;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Implementations;

public class ContractService : IContractService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;

    private readonly IContractQueryObject _contractQueryObject;

    private readonly IGenericRepository<Contract> _contractRepository;

    public ContractService(IUnitOfWorkProvider unitOfWorkProvider,
        IContractQueryObject contractQueryObject,
        IGenericRepository<Contract> contractRepository)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractQueryObject = contractQueryObject;
        _contractRepository = contractRepository;
    }

    public async Task CreateContractWithoutCommit(ContractUpsertDto contractUpsertDto)
    {
        contractUpsertDto.StartDate = DateTime.Now;
        if (contractUpsertDto.State is ContractState.Created or ContractState.Open && contractUpsertDto.PersonId != null)
        {
            contractUpsertDto.State = ContractState.Assigned;
        }
        await _contractRepository.Insert(contractUpsertDto.Adapt<Contract>());
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetAllContracts()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContracts = await _contractRepository.GetAll();
        return returnedContracts.Select(contract => contract.Adapt<ContractDetailedDto>());
    }

    public async Task<ContractDetailedDto> GetContractById(int contractId)
    {
        return (await _contractQueryObject.ExecuteQuery(new ContractFilterDto {Id = contractId})).First();
    }
    
    public async Task<IEnumerable<ContractDetailedDto>> GetContractsFiltered(ContractFilterDto contractFilterDto)
    {
        return await _contractQueryObject.ExecuteQuery(contractFilterDto);
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsByState(ContractState state, int? pageNumber)
    {
        return await _contractQueryObject.ExecuteQuery(new ContractFilterDto
            { State = state, SortCriteria = "StartDate", SortAscending = false, RequestedPageNumber = pageNumber });
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsAssignedToPerson(int personId, int? pageNumber)
    {
        return await _contractQueryObject.ExecuteQuery(new ContractFilterDto
        {
            PersonId = personId, SortCriteria = "StartDate", SortAscending = false, RequestedPageNumber = pageNumber
        });
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsByContractor(int contractorId)
    {
        return await _contractQueryObject.ExecuteQuery(new ContractFilterDto
        {
            ContractorId = contractorId, SortCriteria = "StartDate", SortAscending = false
        });
    }
    
    public async void UpdateContract(ContractUpsertDto contractUpsertDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        UpdateContractWithoutCommit(contractUpsertDto);        
        await uow.CommitAsync();
    }

    public void UpdateContractWithoutCommit(ContractUpsertDto contractUpsertDto)
    {
        if (contractUpsertDto.State == ContractState.Open && contractUpsertDto.PersonId != null)
        {
            contractUpsertDto.State = ContractState.Assigned;
        }
        if (contractUpsertDto.State == ContractState.Assigned && contractUpsertDto.PersonId == null)
        {
            contractUpsertDto.State = ContractState.Open;
        }
        if (contractUpsertDto.EndDate == null &&
            (contractUpsertDto.State is ContractState.Cancelled or ContractState.Unresolved or ContractState.Resolved))
        {
            contractUpsertDto.EndDate = DateTime.Now;
        }
        _contractRepository.Update(contractUpsertDto.Adapt<Contract>());
    }

    public async Task ChangeContractStateWithoutCommit(int contractId, ContractState state)
    {
        var contractToUpdate = await _contractRepository.GetById(contractId);
        contractToUpdate.State = state;
        _contractRepository.Update(contractToUpdate);
    }

    public async Task AssignPersonToContractWithoutCommit(int contractId, int personId)
    {
        var contractToUpdate = await _contractRepository.GetById(contractId);
        contractToUpdate.PersonId = personId;
        contractToUpdate.State = ContractState.Assigned;
        _contractRepository.Update(contractToUpdate);
    }

    public async Task AddContractorToContract(int contractId, int contractorId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var contractToUpdate = await _contractRepository.GetById(contractId);
        contractToUpdate.ContractorId = contractorId;
        _contractRepository.Update(contractToUpdate);
        await uow.CommitAsync();
    }

    public async Task DeleteContract(int contractId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractRepository.Delete(contractId);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<ContractSimpleDto>> GetAllSimpleContracts()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContracts = await _contractRepository.GetAll();
        return returnedContracts.Select(contract => contract.Adapt<ContractSimpleDto>());
    }
}