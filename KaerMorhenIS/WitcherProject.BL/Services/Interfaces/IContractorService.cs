using WitcherProject.BL.DTOs.Contractor;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractorService
{
    Task CreateContractorAsync(ContractorDto contractDto);

    Task<IEnumerable<ContractorDto>> GetAllContractorsAsync();

    Task<ContractorDto> GetContractorByIdAsync(int contractorId);

    Task UpdateContractorAsync(ContractorDto contractorDto);

    Task DeleteContractorAsync(int contractId);
}