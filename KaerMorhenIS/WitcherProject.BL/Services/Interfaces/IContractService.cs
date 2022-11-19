using WitcherProject.BL.DTOs.Contract;
using WitcherProject.DAL.Models.Enums;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractService
{
    Task CreateContractAsync(ContractAddDto contractAddDto);

    Task<IEnumerable<ContractDetailedDto>> GetAllContractsAsync();
    
    Task<IEnumerable<ContractDetailedDto>> GetContractsFilteredAsync(ContractFilterDto contractFilterDto);
    
    Task<IEnumerable<ContractDetailedDto>> GetContractsByStateAsync(ContractState contractState);
    
    Task<IEnumerable<ContractDetailedDto>> GetContractsAssignedToPersonAsync(int personId);

    Task UpdateContractAsync(ContractUpdateDto contractUpdateDto);

    Task ChangeContractStateAsync(int contractId, ContractState state);

    Task AddPersonToContractAsync(int contractId, int personId);
    
    Task AddContractorToContractAsync(int contractId, int contractorId);

    Task DeleteContractAsync(int contractId);
}