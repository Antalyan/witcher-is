using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractRequestAdvancedService
{
    Task ApproveContractRequest(ContractRequestUpdateDto contractRequest, int contractId, int personId);
}