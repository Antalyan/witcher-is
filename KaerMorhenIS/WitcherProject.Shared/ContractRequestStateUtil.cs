using WitcherProject.Shared.Enums;

namespace WitcherProject.Shared;

public static class ContractRequestStateUtil
{
    public static IEnumerable<ContractRequestState> GetAllStates()
    {
        return Enum.GetValues(typeof(ContractRequestState)).Cast<ContractRequestState>();
    }
}