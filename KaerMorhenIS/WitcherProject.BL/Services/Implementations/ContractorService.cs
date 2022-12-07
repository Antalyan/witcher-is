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
    
    public async Task CreateContractorAsync(ContractorDto contractDto)
    {
        var contractorToInsert = contractDto.Adapt<Contractor>();
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractorRepository.Insert(contractorToInsert);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<ContractorDto>> GetAllContractorsAsync()
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContractors = await _contractorRepository.GetAll();
        return returnedContractors.Select(contractor => contractor.Adapt<ContractorDto>());
    }
    
    public async Task<ContractorDto> GetContractorByIdAsync(int contractorId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        var returnedContractor = await _contractorRepository.GetById(contractorId);
        return returnedContractor.Adapt<ContractorDto>();
    }

    public async Task UpdateContractAsync(ContractorDto contractorDto)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        _contractorRepository.Update(contractorDto.Adapt<Contractor>());
        await uow.CommitAsync();
    }

    public async Task DeleteContractorAsync(int contractId)
    {
        await using var uow = _unitOfWorkProvider.CreateUow();
        await _contractorRepository.Delete(contractId);
        await uow.CommitAsync();
    }
}