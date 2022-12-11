namespace WitcherProject.Shared;

public static class RoleNames
{
    public const string Admin = "Admin";

    public const string UserManager = "UserManager";
    
    public const string Witcher = "Witcher";

    public const string ContractManager = "ContractManager";
    
    public static string GetRoles(string[] roles)
    {
        return roles.Aggregate((x, y) => x + ", " + y);
    }

}