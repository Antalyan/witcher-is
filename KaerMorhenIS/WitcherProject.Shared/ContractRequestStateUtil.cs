using WitcherProject.Shared.Enums;

namespace WitcherProject.Shared;

public static class ContractRequestStateUtil
{
    public static IEnumerable<ContractRequestState> GetUserSettableStates()
    {
        // TODO: think through the actuall possible user settable states
        return new List<ContractRequestState> { ContractRequestState.Accepted, ContractRequestState.Approved, ContractRequestState.Declined };
    }
    
    public static IEnumerable<ContractRequestState> GetAllStates()
    {
        return Enum.GetValues(typeof(ContractRequestState)).Cast<ContractRequestState>();
    }
}