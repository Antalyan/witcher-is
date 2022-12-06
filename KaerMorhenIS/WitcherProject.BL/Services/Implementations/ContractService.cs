using Mapster;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;
using Contract = WitcherProject.DAL.Models.Contract;

namespace WitcherProject.BL.Services.Implementations;

public class ContractService : IContractService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;

    private readonly IContractQueryObject _contractQueryObject;

    private readonly IGenericRepository<Contract> _contractRepository;

    public ContractService(IUnitOfWorkProvider unitOfWorkProvider,
        IContractQueryObject contractQueryObject,
        IGenericRepository<Contract> contractRepository,
        IGenericRepository<Contractor> contractorRepository,
        IGenericRepository<ContractRequest> contractRequestRepository)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractQueryObject = contractQueryObject;
        _contractRepository = contractRepository;
    }

    public async Task CreateContractAsync(ContractAddDto contractAddDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        contractAddDto.State = contractAddDto.PersonId != null ? ContractState.Assigned : ContractState.Created;
        await _contractRepository.Insert(contractAddDto.Adapt<Contract>());
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetAllContractsAsync()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContracts = await _contractRepository.GetAll();
        return returnedContracts.Select(contract => contract.Adapt<ContractDetailedDto>());
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsFilteredAsync(ContractFilterDto contractFilterDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        return await _contractQueryObject.ExecuteQuery(contractFilterDto);
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsByStateAsync(ContractState state, int? pageNumber)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        return await _contractQueryObject.ExecuteQuery(new ContractFilterDto
            { State = state, SortCriteria = "StartDate", SortAscending = false, RequestedPageNumber = pageNumber });
    }

    public async Task<IEnumerable<ContractDetailedDto>> GetContractsAssignedToPersonAsync(int personId, int? pageNumber)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        return await _contractQueryObject.ExecuteQuery(new ContractFilterDto
        {
            PersonId = personId, SortCriteria = "StartDate", SortAscending = false, RequestedPageNumber = pageNumber
        });
    }

    public async Task UpdateContractAsync(ContractUpdateDto contractUpdateDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
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