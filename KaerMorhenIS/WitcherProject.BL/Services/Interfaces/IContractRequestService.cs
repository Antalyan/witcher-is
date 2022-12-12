using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractRequestService
{
    Task CreateContractRequestAsync(ContractRequestAddDto contractRequestAddDto);

    Task<IEnumerable<ContractRequestDetailedDto>> GetAllContractRequestsAsync();
    
    Task <ContractRequestDetailedDto> GetContractRequestByIdAsync(int requestId);
    
    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByStateAsync(ContractRequestState state, int? pageNumber);
    
    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByContractAsync(int contractId, int? pageNumber);
    
    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestByPersonAsync(int personId, int? pageNumber);

    Task<IEnumerable<ContractRequestDetailedDto>> GetContractRequestsFilteredAsync(ContractRequestFilterDto contractRequestFilterDto);

    Task UpdateContractRequestAsync(ContractRequestUpdateDto contractRequestUpdateDto);

    Task DeleteContractRequestAsync(int requestId);
}