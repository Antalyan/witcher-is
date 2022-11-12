using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Contractor;

namespace WitcherProject.BL.Services;

public interface IContractRequestService
{
    Task AddContractAsync(ContractAddDto contractAddDto);

    Task<IEnumerable<ContractGetDto>> GetAllContractsAsync();
    
    Task<IEnumerable<ContractGetDto>> GetContractsByContractorAsync(ContractorGetDto contractorGetDto);
    
    Task<IEnumerable<ContractGetDto>> GetContractsAssignedToPersonAsync(ContractGetDto contractGetDto);

    Task UpdateContractAsync(ContractGetDto contractGetDto);
}