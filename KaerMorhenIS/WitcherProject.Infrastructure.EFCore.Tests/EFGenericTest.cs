using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using WitcherProject.DAL;

namespace WitcherProject.Infrastructure.EFCore.Tests;

public abstract class EFGenericTest
{
    protected DbContextOptions<KaerMorhenDBContext> _options;

    protected EFGenericTest()
    {
        // based on https://github.com/Lukas-Razz/CSharp_Demo-QueryObject/blob/master/Demo.QueryObject.Infrastructure.EFCore.Tests/QueryObjectTests.cs

        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        _options = new DbContextOptionsBuilder<KaerMorhenDBContext>()
            .UseInMemoryDatabase($"test_db_{DateTime.Now.ToFileTimeUtc()}")
            .UseInternalServiceProvider(serviceProvider)
            .Options;
        
        
        
    }
}