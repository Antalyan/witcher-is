using Mapster;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Implementations;


public class ContractFacade : IContractFacade
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    private readonly IContractService _contractService;
    private readonly IContractRequestService _contractRequestService;
    private readonly IContractorService _contractorService;

    public ContractFacade(IUnitOfWorkProvider unitOfWorkProvider, IContractService contractService, 
        IContractRequestService contractRequestService, IContractorService contractorService)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractService = contractService;
        _contractRequestService = contractRequestService;
        _contractorService = contractorService;
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
        
        await _contractService.AssignPersonToContractWithoutCommit(contractId, personId);
        await uow.CommitAsync();
    }

    public async Task<bool> DeleteContractorIfNotAssigned(int contractorId)
    {
        var assignedContracts = await _contractService.GetContractsByContractor(contractorId);
        if (assignedContracts.Any()) return false;
        
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractorService.DeleteContractorAsync(contractorId);
        await uow.CommitAsync();
        return true;
    }

    public async Task SaveContract(int? personId, int? contractorId, ContractUpsertDto contractDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        
        var personChanged = contractDto.PersonId == personId;
        contractDto.PersonId = personId;
        contractDto.ContractorId = contractorId;

        if (contractDto.Id is null or -1) // Create
        {
            await _contractService.CreateContractWithoutCommit(contractDto);
        }
        else // Update
        {
            _contractService.UpdateContractWithoutCommit(contractDto);
            if (personChanged)
            {
                var filter = new ContractRequestFilterDto {State = ContractRequestState.Requested, ContractId = contractDto.Id};
                var allOpenRequestsForContract = await _contractRequestService.GetContractRequestsFilteredAsync(filter);
        
                foreach (var request in allOpenRequestsForContract)
                {
                    request.State = request.Person.Id == personId ? ContractRequestState.Approved : ContractRequestState.Declined;
                    _contractRequestService.UpdateWithoutCommitContractRequest(request.Adapt<ContractRequestUpdateDto>());
                }
            }
        }
        
        await uow.CommitAsync();
    }
}