using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractRequestService
{
    Task<IEnumerable<ContractRequestDetailedDto>> GetAllContractRequests();
    
    Task <ContractRequestDetailedDto> GetContractRequestById(int requestId);
    
    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByState(ContractRequestState state, int? pageNumber);
    
    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByContract(int contractId, int? pageNumber);
    
    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByPerson(int personId, int? pageNumber);

    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestsFiltered(ContractRequestFilterDto contractRequestFilterDto);
    
    Task CreateContractRequest(ContractRequestAddDto contractRequestAddDto);
    
    Task UpdateContractRequest(ContractRequestUpdateDto contractRequestUpdateDto);

    void UpdateContractWithoutCommit(ContractRequestUpdateDto contractRequestUpdateDto);

    Task DeleteContractRequest(int requestId);
}