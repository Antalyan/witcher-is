using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.DTOs.Contractor;

namespace WitcherProject.BL.Services;

public interface IContractorService
{
    Task CreateContractorAsync(ContractorDto contractDto);

    Task<IEnumerable<ContractorDto>> GetAllContractorsAsync();

    Task<ContractorDto> GetContractorByIdAsync(int contractorId);

    Task UpdateContractAsync(ContractorDto contractorDto);

    Task DeleteContractorAsync(int contractId);
}