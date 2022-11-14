using WitcherProject.BL.DTOs.Contract;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractService
{
    Task CreateContractAsync(ContractDto contractDto, int contractorId, int personId);

    Task<IEnumerable<ContractDto>> GetAllContractsAsync();
    
    Task<IEnumerable<ContractDto>> GetContractsFiltered(ContractFilterDto contractFilterDto);

    Task UpdateContractAsync(ContractDto contractDto);

    Task AddPersonToContract(int contractId, int personId);
    
    Task AddContractorToContract(int contractId, int contractorId);

    Task DeleteContractAsync(int contractId);


    // TODO: should service also contain methods like add contract to person/contractor?
}