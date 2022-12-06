using Mapster;
using WitcherProject.BL.DTOs.Contractor;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

namespace WitcherProject.BL.Services.Implementations;

public class ContractorService: IContractorService
{
    private readonly IUnitOfWorkProvider _unitOfWorkProvider;
    
    private readonly IGenericRepository<Contract> _contractRepository;
    private readonly IGenericRepository<Contractor> _contractorRepository;
    private readonly IGenericRepository<ContractRequest> _contractRequestRepository;

    public ContractorService(IUnitOfWorkProvider unitOfWorkProvider,
        IGenericRepository<Contract> contractRepository, 
        IGenericRepository<Contractor> contractorRepository,
        IGenericRepository<ContractRequest> contractRequestRepository)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _contractRepository = contractRepository;
        _contractorRepository = contractorRepository;
        _contractRequestRepository = contractRequestRepository;
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