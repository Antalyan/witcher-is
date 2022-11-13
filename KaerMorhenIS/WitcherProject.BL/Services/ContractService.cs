using System.Diagnostics.Contracts;
using Mapster;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Contract;
using WitcherProject.BL.QueryObjects;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.DAL.Models.Enums;
using WitcherProject.Infrastructure.EFCore.Query;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
using Contract = WitcherProject.DAL.Models.Contract;

namespace WitcherProject.BL.Services;

public class ContractService : IContractService
{
    private readonly IUnitOfWorkContracts _contractsUow;
    private readonly KaerMorhenDBContext _context;

    public ContractService(IUnitOfWorkContracts contractsUow, KaerMorhenDBContext context)
    {
        _contractsUow = contractsUow;
        _context = context;
        
        // TODO: setup config if needed
        // TypeAdapterConfig<TSource, TDestination>
        //     .NewConfig()
        //     .Map(dest => dest.FullName,
        //         src => $"{src.FirstName} {src.LastName}");
    }

    public async Task CreateContractAsync(ContractDto contractDto, int contractorId = -1, int personId = -1)
    {
        var contractToInsert = contractDto.Adapt<Contract>();
        if (contractorId != -1)
        {
            contractToInsert.Contractor = await _contractsUow.ContractorRepository.GetById(contractorId);
        }

        if (personId != -1)
        {
            contractToInsert.Person = await _contractsUow.PersonRepository.GetById(personId);
            contractToInsert.State = ContractState.Assigned;
        }
        else
        {
            //TODO: which state should be set? or is it decided above dependent on who calls it?
        }

        await _contractsUow.ContractRepository.Insert(contractToInsert);
        await _contractsUow.CommitAsync();
    }

    public async Task<IEnumerable<ContractDto>> GetAllContractsAsync()
    {
        var returnedContracts = await _contractsUow.ContractRepository.GetAll();
        return returnedContracts.Select(contract => contract.Adapt<ContractDto>());
    }

    public async Task<IEnumerable<ContractDto>> GetContractsFiltered(ContractFilterDto contractFilterDto)
        => await new ContractQueryObject(_context).ExecuteQuery(contractFilterDto);

    public async Task<IEnumerable<ContractDto>> GetContractsByContractorAsync(int contractorId)
    //TODO: set order by, is this method even necessary as filter can be set above?
        => await new ContractQueryObject(_context).ExecuteQuery(new ContractFilterDto {ContractorId = contractorId});

    public async Task<IEnumerable<ContractDto>> GetContractsAssignedToPersonAsync(int personId)
    //TODO: set order by, is this method even necessary as filter can be set above?
        => await new ContractQueryObject(_context).ExecuteQuery(new ContractFilterDto {PersonId = personId});

    public async Task UpdateContractAsync(ContractDto contractDto)
    {
        _contractsUow.ContractRepository.Update(contractDto.Adapt<Contract>());
        await _contractsUow.CommitAsync();
    }

    public async Task AddPersonToContract(int contractId, int personId)
    {
        var contractToUpdate = await _contractsUow.ContractRepository.GetById(contractId);
        contractToUpdate.PersonId = personId;
        _contractsUow.ContractRepository.Update(contractToUpdate);
        await _contractsUow.CommitAsync();
    }

    public async Task AddContractorToContract(int contractId, int contractorId)
    {
        var contractToUpdate = await _contractsUow.ContractRepository.GetById(contractId);
        contractToUpdate.ContractorId = contractorId;
        _contractsUow.ContractRepository.Update(contractToUpdate);
        await _contractsUow.CommitAsync();
    }

    public async Task DeleteContractAsync(int contractId)
    {
        await _contractsUow.ContractRepository.Delete(contractId);
        await _contractsUow.CommitAsync();
    }
}