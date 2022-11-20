using WitcherProject.BL.DTOs.Contract;

namespace WitcherProject.BL.QueryObjects;

public interface IContractQueryObject
{
    Task<IEnumerable<ContractDetailedDto>> ExecuteQuery(ContractFilterDto filter);
}