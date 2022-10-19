using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.DAL.Models.Enums;
using WitcherProject.Infrastructure.EFCore.Repository;
using Xunit;

namespace WitcherProject.Infrastructure.EFCore.Tests;

public class EFGenericRepositoryTests
{
    private DbContextOptions<KaerMorhenDBContext> _options;

    // based on https://github.com/Lukas-Razz/CSharp_Demo-QueryObject/blob/master/Demo.QueryObject.Infrastructure.EFCore.Tests/QueryObjectTests.cs
    public EFGenericRepositoryTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        _options = new DbContextOptionsBuilder<KaerMorhenDBContext>()
            .UseInMemoryDatabase($"test_db_{DateTime.Now.ToFileTimeUtc()}")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        using var kaerMorhenDbContext = new KaerMorhenDBContext(_options);
     
        kaerMorhenDbContext.Add(new Contractor
            {Id = 1, Name = "FullName", Surname = "FullSurname", Contracts = new List<Contract>()});
        kaerMorhenDbContext.Add(new Contractor
            {Id = 2, Name = "NoSurname", Contracts = new List<Contract>()});

        kaerMorhenDbContext.SaveChanges();
    }

    [Fact]
    public async Task ShouldGetAllContractors()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);

        var allContractors = await repoUnderTest.GetAll();
        
        Assert.True(allContractors.Count() == 2);
    }

    [Fact]
    public async Task ShouldInsertContractor()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);
        var contractor = new Contractor
            {Name = "NewContractorName", Surname = "NewContractorSurname", Contracts = new List<Contract>()};

        await repoUnderTest.Insert(contractor);
        await dbContext.SaveChangesAsync();

        var allContractors = await repoUnderTest.GetAll();
        Assert.True(allContractors.Count() == 3);
    }

    [Fact]
    public async Task ShouldGetContractorById()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);
    
        var contractor = await repoUnderTest.GetById(1);
        
        Assert.True(contractor.Id == 1);
        Assert.Equal("FullName", contractor.Name);
    }

    [Fact]
    public async Task ShouldDeleteContractor()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);

        await repoUnderTest.Delete(1);
        await dbContext.SaveChangesAsync();
        
        var allContractors = await repoUnderTest.GetAll();
        Assert.True(allContractors.Count() == 1);
    }

    [Fact]
    public async Task ShouldUpdateContractor()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);

        var contractor = await repoUnderTest.GetById(1);
        var oldName = contractor.Name;
        const string updatedName = "updatedName";
        contractor.Name = updatedName;

        repoUnderTest.Update(contractor);
        await dbContext.SaveChangesAsync();
        
        var updatedContractor = await repoUnderTest.GetById(1);
        
        Assert.NotEqual(oldName, updatedContractor.Name);
        Assert.Equal(updatedName, updatedContractor.Name);
    }
    
    // possible further test cases: lazy loading, nullables, navigation property...
}