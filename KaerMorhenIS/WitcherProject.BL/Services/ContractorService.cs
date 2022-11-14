using Mapster;
using WitcherProject.BL.DTOs.Contractor;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;

namespace WitcherProject.BL.Services;

public class ContractorService: IContractorService
{
    private readonly IUnitOfWorkContracts _contractsUow;

    public ContractorService(IUnitOfWorkContracts contractsUow)
    {
        _contractsUow = contractsUow;
    }
    
    public async Task CreateContractorAsync(ContractorDto contractDto)
    {
        var contractorToInsert = contractDto.Adapt<Contractor>();
        await _contractsUow.ContractorRepository.Insert(contractorToInsert);
        await _contractsUow.CommitAsync();
    }

    public async Task<IEnumerable<ContractorDto>> GetAllContractorsAsync()
    {
        var returnedContractors = await _contractsUow.ContractorRepository.GetAll();
        return returnedContractors.Select(contractor => contractor.Adapt<ContractorDto>());
    }
    
    public async Task<ContractorDto> GetContractorByIdAsync(int contractorId)
    {
        var returnedContractor = await _contractsUow.ContractorRepository.GetById(contractorId);
        return returnedContractor.Adapt<ContractorDto>();
    }

    public async Task UpdateContractAsync(ContractorDto contractorDto)
    {
        _contractsUow.ContractorRepository.Update(contractorDto.Adapt<Contractor>());
        await _contractsUow.CommitAsync();
    }

    public async Task DeleteContractorAsync(int contractId)
    {
        await _contractsUow.ContractorRepository.Delete(contractId);
        await _contractsUow.CommitAsync();
    }
}