using Mapster;
using WitcherProject.BL.DTOs.Contractor;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

namespace WitcherProject.BL.Services.Implementations;

public class ContractorService: IContractorService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    
    private readonly IGenericRepository<Contractor> _contractorRepository;

    public ContractorService(IUnitOfWorkProvider unitOfWorkProvider,
        IGenericRepository<Contractor> contractorRepository)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractorRepository = contractorRepository;
    }
    
    public async Task CreateContractor(ContractorDto contractDto)
    {
        var contractorToInsert = contractDto.Adapt<Contractor>();
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractorRepository.Insert(contractorToInsert);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<ContractorDto>> GetAllContractors()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContractors = (await _contractorRepository.GetAll())
            .OrderBy(contractor => contractor.Surname).ThenBy(contractor => contractor.Name);
        return returnedContractors.Select(contractor => contractor.Adapt<ContractorDto>());
    }
    
    public async Task<ContractorDto> GetContractorById(int contractorId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContractor = await _contractorRepository.GetById(contractorId);
        return returnedContractor.Adapt<ContractorDto>();
    }

    public async Task UpdateContractor(ContractorDto contractorDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        _contractorRepository.Update(contractorDto.Adapt<Contractor>());
        await uow.CommitAsync();
    }

    public async Task DeleteContractor(int contractId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractorRepository.Delete(contractId);
        await uow.CommitAsync();
    }
}