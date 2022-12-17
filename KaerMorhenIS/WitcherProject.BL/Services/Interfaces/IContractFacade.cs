using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractFacade
{
    Task ApproveContractRequest(ContractRequestUpdateDto contractRequest, int contractId, int personId);

    Task<bool> DeleteContractorIfNotAssigned(int contractorId);
}