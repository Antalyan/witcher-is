using Mapster;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.DAL.Models;

namespace WitcherProject.BL;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.ForType<Role, RoleDto>().TwoWays()
            .Map(rd => rd.UserRoleDtos, r => r.UserRoles)
            .PreserveReference(true);

        config.ForType<Person, PersonCompleteDto>().TwoWays()
            .Map(pcd => pcd.UserRoleDtos, p => p.UserRoles)
            .PreserveReference(true)
            .Map(pcd => pcd.Contracts, p => p.Contracts)
            .PreserveReference(true);
    }
}
