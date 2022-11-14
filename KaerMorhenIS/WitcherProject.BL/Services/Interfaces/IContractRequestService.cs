using WitcherProject.BL.DTOs.ContractRequest;
using WitcherProject.DAL.Models.Enums;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractRequestService
{
    Task CreateContractRequestAsync(ContractRequestDto contractRequestDto);

    Task<IEnumerable<ContractRequestDto>> GetAllContractRequestsAsync();
    
    Task <ContractRequestDto> GetContractRequestByIdAsync(int requestId);
    
    Task<IEnumerable<ContractRequestDto>> GetContractRequestByState(ContractRequestState state, int? pageNumber);
    
    Task<IEnumerable<ContractRequestDto>> GetContractRequestByContract(int contractId, int? pageNumber);
    
    Task<IEnumerable<ContractRequestDto>> GetContractRequestByPerson(int personId, int? pageNumber);

    Task UpdateContractRequestAsync(ContractRequestDto contractRequestDto);

    Task DeleteContractRequestAsync(int requestId);
}