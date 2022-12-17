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
    
    private readonly IContractRequestQueryObject _contractRequestQueryObject;
    
    private readonly IGenericRepository<ContractRequest> _contractRequestRepository;
    
    public ContractRequestService(IUnitOfWorkProvider unitOfWorkProvider,
        IContractRequestQueryObject contractRequestQueryObject,
        IGenericRepository<ContractRequest> contractRequestRepository)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractRequestQueryObject = contractRequestQueryObject;
        _contractRequestRepository = contractRequestRepository;
    }
    public async Task CreateContractRequestAsync(ContractRequestAddDto contractRequestAddDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var requestToInsert = contractRequestAddDto.Adapt<ContractRequest>();
        requestToInsert.CreatedOn = DateTime.Now;
        await _contractRequestRepository.Insert(requestToInsert);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetAllContractRequestsAsync()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedRequests = await _contractRequestRepository.GetAll();
        return returnedRequests.Select(request => request.Adapt<ContractRequestDetailedDto>());
    }

    public async Task<ContractRequestDetailedDto> GetContractRequestByIdAsync(int requestId)
    {
        // await using var uow = _unitOfWorkProvider.CreateUow();
        // var returnedRequests = await _contractRequestRepository.GetById(requestId);
        // return returnedRequests.Adapt<ContractRequestDetailedDto>();

        return (await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto {Id = requestId})).First();
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByStateAsync(ContractRequestState state, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { State = state, SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByContractAsync(int contractId, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { ContractId = contractId, SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByPersonAsync(int personId, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { PersonId = personId, SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestsFilteredAsync(ContractRequestFilterDto contractRequestFilterDto)
    {
        return await _contractRequestQueryObject.ExecuteQuery(contractRequestFilterDto);
    }
    
    public async Task UpdateContractRequestAsync(ContractRequestUpdateDto contractRequestUpdateDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        _contractRequestRepository.Update(contractRequestUpdateDto.Adapt<ContractRequest>());
        await uow.CommitAsync();
    }

    public void UpdateWithoutCommitContractRequest(ContractRequestUpdateDto contractRequestUpdateDto)
    {
        _contractRequestRepository.Update(contractRequestUpdateDto.Adapt<ContractRequest>());
    }

    public async Task DeleteContractRequestAsync(int requestId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractRequestRepository.Delete(requestId);
        await uow.CommitAsync();
    }
}