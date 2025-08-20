using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using WitcherProject.DAL;

namespace WitcherProject.Infrastructure.EFCore;

public static class ServiceCollectionExtensions
{
    private static string BuildPostgresqlConnectionString(string conn)
    {
        if (!conn.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
            !conn.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)) return conn;
        var uri = new Uri(conn);
        var userInfo = uri.UserInfo.Split(':');

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port,
            Database = uri.AbsolutePath.TrimStart('/'),
            Username = userInfo[0],
            Password = userInfo[1],
            SslMode = SslMode.Require,
            Pooling = true,
        };
        return builder.ConnectionString;

    }
    
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var usedDb = config.GetSection("ActiveDb").Value;
        if (usedDb == "Postgresql")
        {
            var connectionString = BuildPostgresqlConnectionString(config.GetConnectionString("PostgresqlMain"));
            services.AddDbContextFactory<KaerMorhenDBContext>(
                options => options.UseNpgsql(connectionString).EnableSensitiveDataLogging(), ServiceLifetime.Scoped);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        } else if (usedDb == "SqlServer")
        {
            var connectionString = config.GetConnectionString("SqlServerMain");
            services.AddDbContextFactory<KaerMorhenDBContext>(
                options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging(), ServiceLifetime.Scoped);
        }
        else
        {
            throw new Exception($"Unknown db type in appsettings: {usedDb}");
        }

        return services;
    }
}
