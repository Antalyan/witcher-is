using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Contractor;

namespace WitcherProject.BL.Services;

public class ContractService: IContractService
{
    // TODO
    public Task AddContractAsync(ContractAddDto contractAddDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ContractGetDto>> GetAllContractsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ContractGetDto>> GetContractsByContractorAsync(ContractorGetDto contractorGetDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ContractGetDto>> GetContractsAssignedToPersonAsync(ContractGetDto contractGetDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateContractAsync(ContractGetDto contractGetDto)
    {
        throw new NotImplementedException();
    }
}