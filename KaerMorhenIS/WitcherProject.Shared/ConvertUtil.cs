using Microsoft.AspNetCore.Identity;

namespace WitcherProject.Shared;

public static class ConvertUtil
{
    public static string AggregateErrors(IEnumerable<IdentityError> errors)
    {
        return errors.Select(err => err.Description)
            .Aggregate((x, y) => x + ", " + y);
    }
    
    
}