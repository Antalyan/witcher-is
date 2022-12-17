using WitcherProject.BL.DTOs.Contractor;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractorService
{
    Task<IEnumerable<ContractorDto>> GetAllContractors();

    Task<ContractorDto> GetContractorById(int contractorId);
    
    Task CreateContractor(ContractorDto contractDto);

    Task UpdateContractor(ContractorDto contractorDto);

    Task DeleteContractor(int contractId);
}