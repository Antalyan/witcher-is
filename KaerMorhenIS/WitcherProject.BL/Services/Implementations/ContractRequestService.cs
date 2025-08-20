using Mapster;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Implementations;

public class ContractRequestService : IContractRequestService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    private readonly IRepositoryProvider _repositoryProvider;
    
    private readonly IContractRequestQueryObject _contractRequestQueryObject;
    
    public ContractRequestService(IUnitOfWorkProvider unitOfWorkProvider,
        IContractRequestQueryObject contractRequestQueryObject,
        IRepositoryProvider repositoryProvider)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractRequestQueryObject = contractRequestQueryObject;
        _repositoryProvider = repositoryProvider;
    }
    public async Task CreateContractRequest(ContractRequestAddDto contractRequestAddDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var repository = _repositoryProvider.GetRepository<ContractRequest>(uow);
        var requestToInsert = contractRequestAddDto.Adapt<ContractRequest>();
        requestToInsert.CreatedOn = DateTime.Now;
        await repository.Insert(requestToInsert);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetAllContractRequests()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var repository = _repositoryProvider.GetRepository<ContractRequest>(uow);
        var returnedRequests = await repository.GetAll();
        return returnedRequests.Select(request => request.Adapt<ContractRequestDetailedDto>());
    }

    public async Task<ContractRequestDetailedDto> GetContractRequestById(int requestId)
    {
        return (await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto {Id = requestId})).First();
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByState(ContractRequestState state, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { State = state, SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByContract(int contractId, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { ContractId = contractId, SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByPerson(int personId, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { PersonId = personId, SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestsFiltered(ContractRequestFilterDto contractRequestFilterDto)
    {
        return await _contractRequestQueryObject.ExecuteQuery(contractRequestFilterDto);
    }
    
    public async Task UpdateContractRequest(ContractRequestUpdateDto contractRequestUpdateDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var repository = _repositoryProvider.GetRepository<ContractRequest>(uow);
        repository.Update(contractRequestUpdateDto.Adapt<ContractRequest>());
        await uow.CommitAsync();
    }

    public void UpdateContractWithoutCommit(ContractRequestUpdateDto contractRequestUpdateDto, IUnitOfWork uow)
    {
        var repository = _repositoryProvider.GetRepository<ContractRequest>(uow);
        repository.Update(contractRequestUpdateDto.Adapt<ContractRequest>());
    }

    public async Task DeleteContractRequest(int requestId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var repository = _repositoryProvider.GetRepository<ContractRequest>(uow);
        await repository.Delete(requestId);
        await uow.CommitAsync();
    }
}