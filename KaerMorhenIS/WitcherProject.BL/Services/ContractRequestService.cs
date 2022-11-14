using Mapster;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.QueryObjects;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.DAL.Models.Enums;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;

namespace WitcherProject.BL.Services;

public class ContractRequestService : IContractRequestService
{
    private readonly IUnitOfWorkContracts _contractsUow;
    private readonly KaerMorhenDBContext _context;

    public ContractRequestService(IUnitOfWorkContracts contractsUow, KaerMorhenDBContext context)
    {
        _contractsUow = contractsUow;
        _context = context;
    }

    public async Task CreateContractRequestAsync(ContractRequestDto contractRequestDto)
    {
        var requestToInsert = contractRequestDto.Adapt<ContractRequest>();
        await _contractsUow.ContractRequestRepository.Insert(requestToInsert);
        await _contractsUow.CommitAsync();
    }

    public async Task<IEnumerable<ContractRequestDto>> GetAllContractRequestsAsync()
    {
        var returnedRequests = await _contractsUow.ContractRequestRepository.GetAll();
        return returnedRequests.Select(request => request.Adapt<ContractRequestDto>());
    }

    public async Task<ContractRequestDto> GetContractRequestByIdAsync(int requestId)
    {
        var returnedRequests = await _contractsUow.ContractRequestRepository.GetById(requestId);
        return returnedRequests.Adapt<ContractRequestDto>();
    }

    public async Task<IEnumerable<ContractRequestDto>> GetContractRequestByState(ContractRequestState state, int? pageNumber = null)
        => await new ContractRequestQueryObject(_context).ExecuteQuery(new ContractRequestFilterDto()
            { State = state, SortCriteria = "CreatedOn", SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDto>> GetContractRequestByContract(int contractId, int? pageNumber = null)
        => await new ContractRequestQueryObject(_context).ExecuteQuery(new ContractRequestFilterDto()
            { ContractId = contractId, SortCriteria = "CreatedOn", SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task<IEnumerable<ContractRequestDto>> GetContractRequestByPerson(int personId, int? pageNumber = null)
        => await new ContractRequestQueryObject(_context).ExecuteQuery(new ContractRequestFilterDto()
            { PersonId = personId, SortCriteria = "CreatedOn", SortAscending = false, RequestedPageNumber = pageNumber});

    public async Task UpdateContractRequestAsync(ContractRequestDto contractRequestDto)
    {
        _contractsUow.ContractRequestRepository.Update(contractRequestDto.Adapt<ContractRequest>());
        await _contractsUow.CommitAsync();
    }

    public async Task DeleteContractRequestAsync(int requestId)
    {
        await _contractsUow.ContractRequestRepository.Delete(requestId);
        await _contractsUow.CommitAsync();
    }
}