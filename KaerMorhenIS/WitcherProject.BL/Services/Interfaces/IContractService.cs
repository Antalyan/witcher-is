using WitcherProject.BL.DTOs.Contract;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;

namespace WitcherProject.BL.Services.Interfaces;

public interface IContractService
{
    Task<IEnumerable<ContractDetailedDto>> GetAllContracts();

    Task<ContractDetailedDto> GetContractById(int id);

    Task<IEnumerable<ContractDetailedDto>> GetContractsFiltered(ContractFilterDto contractFilterDto);

    Task<IEnumerable<ContractDetailedDto>> GetContractsByState(ContractState contractState, int? pageNumber = null);

    Task<IEnumerable<ContractDetailedDto>> GetContractsAssignedToPerson(int personId, int? pageNumber = null);

    Task<IEnumerable<ContractDetailedDto>> GetContractsByContractor(int contractorId);

    Task CreateContractWithoutCommit(ContractUpsertDto contractAddDto, IUnitOfWork uow);
    
    void UpdateContract(ContractUpsertDto contractUpsertDto);

    void UpdateContractWithoutCommit(ContractUpsertDto contractUpsertDto, IUnitOfWork uow);

    Task ChangeContractStateWithoutCommit(int contractId, ContractState state, IUnitOfWork uow);

    Task AssignPersonToContractWithoutCommit(int contractId, int personId, IUnitOfWork uow);
    
    Task AddContractorToContract(int contractId, int contractorId);

    Task DeleteContract(int contractId);

    Task<IEnumerable<ContractSimpleDto>> GetAllSimpleContracts();
}