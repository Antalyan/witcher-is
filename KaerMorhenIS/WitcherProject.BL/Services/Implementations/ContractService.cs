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

    public async Task CreateContractAsync(ContractAddDto contractAddDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        contractAddDto.StartDate = DateTime.Now;
        if (contractAddDto.State is ContractState.Created or ContractState.Open && contractAddDto.PersonId != null)
        {
            contractAddDto.State = ContractState.Assigned;
        }
        await _contractRepository.Insert(contractAddDto.Adapt<Contract>());
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetAllContractsAsync()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContracts = await _contractRepository.GetAll();
        return returnedContracts.Select(contract => contract.Adapt<ContractDetailedDto>());
    }

    public async Task<ContractDetailedDto> GetContractByIdAsync(int contractId)
    {
        return (await _contractQueryObject.ExecuteQuery(new ContractFilterDto {Id = contractId})).First();
    }
    
    public async Task<IEnumerable<ContractDetailedDto>> GetContractsFilteredAsync(ContractFilterDto contractFilterDto)
    {
        return await _contractQueryObject.ExecuteQuery(contractFilterDto);
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsByStateAsync(ContractState state, int? pageNumber)
    {
        return await _contractQueryObject.ExecuteQuery(new ContractFilterDto
            { State = state, SortCriteria = "StartDate", SortAscending = false, RequestedPageNumber = pageNumber });
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsAssignedToPersonAsync(int personId, int? pageNumber)
    {
        return await _contractQueryObject.ExecuteQuery(new ContractFilterDto
        {
            PersonId = personId, SortCriteria = "StartDate", SortAscending = false, RequestedPageNumber = pageNumber
        });
    }

    public async Task UpdateContractAsync(ContractUpdateDto contractUpdateDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        
        if (contractUpdateDto.State == ContractState.Open && contractUpdateDto.PersonId != null)
        {
            contractUpdateDto.State = ContractState.Assigned;
        }
        if (contractUpdateDto.EndDate == null &&
            (contractUpdateDto.State is ContractState.Cancelled or ContractState.Unresolved or ContractState.Resolved))
        {
            contractUpdateDto.EndDate = DateTime.Now;
        }
        
        _contractRepository.Update(contractUpdateDto.Adapt<Contract>());
        await uow.CommitAsync();
    }

    public async Task ChangeContractStateAsync(int contractId, ContractState state)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var contractToUpdate = await _contractRepository.GetById(contractId);
        contractToUpdate.State = state;
        _contractRepository.Update(contractToUpdate);
        await uow.CommitAsync();
    }

    public async Task AddPersonToContractAsync(int contractId, int personId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var contractToUpdate = await _contractRepository.GetById(contractId);
        contractToUpdate.PersonId = personId;
        _contractRepository.Update(contractToUpdate);
        await uow.CommitAsync();
    }

    public async Task AddContractorToContractAsync(int contractId, int contractorId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var contractToUpdate = await _contractRepository.GetById(contractId);
        contractToUpdate.ContractorId = contractorId;
        _contractRepository.Update(contractToUpdate);
        await uow.CommitAsync();
    }

    public async Task DeleteContractAsync(int contractId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractRepository.Delete(contractId);
        await uow.CommitAsync();
    }
}