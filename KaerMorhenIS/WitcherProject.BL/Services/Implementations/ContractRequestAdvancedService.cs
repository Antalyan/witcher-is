using Mapster;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Implementations;


public class ContractRequestAdvancedService : IContractRequestAdvancedService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    private readonly IContractService _contractService;
    private readonly IContractRequestService _contractRequestService;

    public ContractRequestAdvancedService(IUnitOfWorkProvider unitOfWorkProvider, IContractService contractService, IContractRequestService contractRequestService)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractService = contractService;
        _contractRequestService = contractRequestService;
    }

    public async Task ApproveContractRequest(ContractRequestUpdateDto contractRequest, int contractId, int personId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();

        _contractRequestService.UpdateWithoutCommitContractRequest(contractRequest);

        // set declined for all other requesters for this particular contract
        var filter = new ContractRequestFilterDto {State = ContractRequestState.Requested, ContractId = contractId};
        var allOpenRequestsForContract = await _contractRequestService.GetContractRequestsFilteredAsync(filter);

        allOpenRequestsForContract = allOpenRequestsForContract.Where(cr => cr.Id != contractRequest.Id).ToList();
        
        
        foreach (var request in allOpenRequestsForContract)
        {
            request.State = ContractRequestState.Declined;
            _contractRequestService.UpdateWithoutCommitContractRequest(request.Adapt<ContractRequestUpdateDto>());
        }

        // change contract state to ASSIGNED and add the person to the contract
        var requestedContract = await _contractService.GetContractByIdAsync(contractId);
        requestedContract.State = ContractState.Assigned;
        requestedContract.Person = new PersonSimpleDto {Id = personId};   // todo - will this work fine?
        _contractService.UpdateWithoutCommitContract(requestedContract.Adapt<ContractUpdateDto>());

        await uow.CommitAsync();
    }
}