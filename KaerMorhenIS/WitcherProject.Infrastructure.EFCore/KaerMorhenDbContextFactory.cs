using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WitcherProject.Infrastructure.EFCore;

public class KaerMorhenDbContextFactory : IDesignTimeDbContextFactory<KaerMorhenDBContext>
{
    public KaerMorhenDBContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var services = new ServiceCollection();
        services.ConfigureDatabase(config);

        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IDbContextFactory<KaerMorhenDBContext>>();

        return factory.CreateDbContext();
    }
}
