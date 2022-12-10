using WitcherProject.Shared.Enums;

namespace WitcherProject.Shared;

public static class ContractStateUtil
{
    public static IEnumerable<ContractState> GetUserSettableStates()
    {
        return new List<ContractState> { ContractState.Assigned, ContractState.Resolved, ContractState.Unresolved };
    }
}