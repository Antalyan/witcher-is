using WitcherProject.Shared.Enums;

namespace WitcherProject.Shared;

public static class ContractRequestStateUtil
{
    public static IEnumerable<ContractRequestState> GetAllStates()
    {
        return new List<ContractRequestState>
            { ContractRequestState.Requested, ContractRequestState.Approved, ContractRequestState.Declined };
    }
}