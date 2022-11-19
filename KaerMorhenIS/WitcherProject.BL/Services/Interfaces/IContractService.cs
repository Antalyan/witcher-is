using WitcherProject.BL.DTOs.Contract;
using WitcherProject.DAL.Models.Enums;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractService
{
    Task CreateContractAsync(ContractAddDto contractAddDto);

    Task<IEnumerable<ContractUpdateDto>> GetAllContractsAsync();
    
    Task<IEnumerable<ContractUpdateDto>> GetContractsByStateAsync(ContractState contractState);
    
    Task<IEnumerable<ContractUpdateDto>> GetContractsByPersonAsync(int personId);

    Task UpdateContractAsync(ContractUpdateDto contractUpdateDto);

    Task ChangeContractStateAsync(ContractUpdateDto contractUpdateDto, int personId);

    Task AddPersonToContractAsync(ContractUpdateDto contractUpdateDto, int personId);
    
    Task AddContractorToContractAsync(ContractUpdateDto contractUpdateDto, int contractorId);

    Task DeleteContractAsync(int contractId);
}