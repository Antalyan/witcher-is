using WitcherProject.BL.DTOs.ContractRequest;

namespace WitcherProject.BL.QueryObjects;

public interface IContractRequestQueryObject
{
    Task<IEnumerable<ContractRequestDetailedDto>> ExecuteQuery(ContractRequestFilterDto filter);
}