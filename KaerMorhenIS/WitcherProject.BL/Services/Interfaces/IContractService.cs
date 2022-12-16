using WitcherProject.BL.DTOs.Contract;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractService
{
    Task CreateContractAsync(ContractAddDto contractAddDto);

    Task<IEnumerable<ContractDetailedDto>> GetAllContractsAsync();

    Task<ContractDetailedDto> GetContractByIdAsync(int id);

    Task<IEnumerable<ContractDetailedDto>> GetContractsFilteredAsync(ContractFilterDto contractFilterDto);
    
    Task<IEnumerable<ContractDetailedDto>> GetContractsByStateAsync(ContractState contractState, int? pageNumber = null);
    
    Task<IEnumerable<ContractDetailedDto>> GetContractsAssignedToPersonAsync(int personId, int? pageNumber = null);

    Task UpdateContractAsync(ContractUpdateDto contractUpdateDto);

    Task ChangeContractStateAsync(int contractId, ContractState state);

    Task AddPersonToContractAsync(int contractId, int personId);
    
    Task AddContractorToContractAsync(int contractId, int contractorId);

    Task DeleteContractAsync(int contractId);

    Task<IEnumerable<ContractSimpleDto>> GetAllSimpleContractsAsync();
}