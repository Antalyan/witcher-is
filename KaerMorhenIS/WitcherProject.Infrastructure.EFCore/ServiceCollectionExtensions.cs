using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WitcherProject.DAL;

namespace WitcherProject.Infrastructure.EFCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var usedDb = config.GetSection("ActiveDb").Value;
        if (usedDb == "Postgresql")
        {
            var connectionString = config.GetConnectionString("PostgresqlMain");
            services.AddDbContextFactory<KaerMorhenDBContext>(
                options => options.UseNpgsql(connectionString), ServiceLifetime.Transient);
        } else if (usedDb == "SqlServer")
        {
            var connectionString = config.GetConnectionString("SqlServerMain");
            services.AddDbContextFactory<KaerMorhenDBContext>(
                options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
        }
        else
        {
            throw new Exception($"Unknown db type in appsettings: {usedDb}");
        }

        return services;
    }
}
