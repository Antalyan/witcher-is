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

public class EFGenericRepositoryTests: EFGenericTest
{
    public EFGenericRepositoryTests()
    {
        using var kaerMorhenDbContext = new KaerMorhenDBContext(_options);
     
        kaerMorhenDbContext.Add(new Contractor
            {Id = 1, Name = "FullName", Surname = "FullSurname", Contracts = new List<Contract>()});
        kaerMorhenDbContext.Add(new Contractor
            {Id = 2, Name = "NoSurname", Contracts = new List<Contract>()});

        kaerMorhenDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetAllContractors_ReturnsTwo()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);

        var allContractors = await repoUnderTest.GetAll();
        
        Assert.True(allContractors.Count() == 2);
    }

    [Fact]
    public async Task InsertContractor_DbReturnsThree()
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
    public async Task Get_ContractById_DbReturnsIdOne()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);
    
        var contractor = await repoUnderTest.GetById(1);
        
        Assert.True(contractor.Id == 1);
        Assert.Equal("FullName", contractor.Name);
    }

    [Fact]
    public async Task Delete_OneLeftAfterGet()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContext);

        await repoUnderTest.Delete(1);
        await dbContext.SaveChangesAsync();
        
        var allContractors = await repoUnderTest.GetAll();
        Assert.True(allContractors.Count() == 1);
    }

    [Fact]
    public async Task Update_Name_ReturnsUpdatedName()
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
    
    
    [Fact]
    public async Task InsertContractViaContractor_DbReturnsOne()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        var repoUnderTestContractors = new EFGenericRepository<Contractor>(dbContext);

        var contractor = await repoUnderTestContractors.GetById(1);
        const string insertedContractName = "Find Yennefer";
        contractor.Contracts = new List<Contract>
        {
            new()
            {
                Name = insertedContractName, Description = "Look everywhere!", State = ContractState.Accepted,
                Location = "White Orchard", Deadline = new DateTime(2050, 12, 1)
            }
        };

        repoUnderTestContractors.Update(contractor);
        await dbContext.SaveChangesAsync();
        
        var updatedContractor = await repoUnderTestContractors.GetById(1);
        
        Assert.True(updatedContractor.Contracts.Count == 1);
        Assert.Equal(updatedContractor.Contracts[0].Name, insertedContractName);
        
        var repoUnderTestContracts = new EFGenericRepository<Contract>(dbContext);
        var returnedContracts = await repoUnderTestContracts.GetAll();

        Assert.Equal(returnedContracts.First().Name, insertedContractName);
    }
    
    // possible further test cases: lazy loading, nullables, navigation property...
}