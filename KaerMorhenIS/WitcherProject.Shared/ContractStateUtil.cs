using WitcherProject.Shared.Enums;

namespace WitcherProject.Shared;

public static class ContractStateUtil
{
    public static IEnumerable<ContractState> GetUserSettableStates(ContractState currentState)
    {
        var result = new List<ContractState>
            { ContractState.Assigned, ContractState.Resolved, ContractState.Unresolved };
        if (!result.Contains(currentState))
        {
            result.Add(currentState);
        }

        return result;
    }
    
    public static IEnumerable<ContractState> GetAllStates()
    {
        return Enum.GetValues(typeof(ContractState)).Cast<ContractState>();
    }
}