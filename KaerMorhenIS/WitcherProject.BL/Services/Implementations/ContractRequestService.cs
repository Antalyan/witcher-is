using Mapster;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Implementations;

public class ContractRequestService : IContractRequestService
{
    private readonly IUnitOfWorkContracts _contractsUow;
    private readonly IContractRequestQueryObject _contractRequestQueryObject;


    public ContractRequestService(IUnitOfWorkContracts contractsUow, IContractRequestQueryObject contractRequestQueryObject)
    {
        _contractsUow = contractsUow;
        _contractRequestQueryObject = contractRequestQueryObject;
    }

    public async Task CreateContractRequestAsync(ContractRequestAddDto contractRequestAddDto)
    {
        var requestToInsert = contractRequestAddDto.Adapt<ContractRequest>();
        requestToInsert.CreatedOn = DateTime.Now;
        await _contractsUow.ContractRequestRepository.Insert(requestToInsert);
        await _contractsUow.CommitAsync();
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetAllContractRequestsAsync()
    {
        var returnedRequests = await _contractsUow.ContractRequestRepository.GetAll();
        return returnedRequests.Select(request => request.Adapt<ContractRequestDetailedDto>());
    }

    public async Task<ContractRequestDetailedDto> GetContractRequestByIdAsync(int requestId)
    {
        var returnedRequests = await _contractsUow.ContractRequestRepository.GetById(requestId);
        return returnedRequests.Adapt<ContractRequestDetailedDto>();
    }

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByStateAsync(ContractRequestState state, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { State = state, SortCriteria = "CreatedOn", SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByContractAsync(int contractId, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { ContractId = contractId, SortCriteria = "CreatedOn", SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByPersonAsync(int personId, int? pageNumber = null)
        => await _contractRequestQueryObject.ExecuteQuery(new ContractRequestFilterDto()
            { PersonId = personId, SortCriteria = "CreatedOn", SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task UpdateContractRequestAsync(ContractRequestUpdateDto contractRequestUpdateDto)
    {
        _contractsUow.ContractRequestRepository.Update(contractRequestUpdateDto.Adapt<ContractRequest>());
        await _contractsUow.CommitAsync();
    }

    public async Task DeleteContractRequestAsync(int requestId)
    {
        await _contractsUow.ContractRequestRepository.Delete(requestId);
        await _contractsUow.CommitAsync();
    }
}