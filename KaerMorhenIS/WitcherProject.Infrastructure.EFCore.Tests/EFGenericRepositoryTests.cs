using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Shared.Enums;
using Xunit;

namespace WitcherProject.Infrastructure.EFCore.Tests;

public class EFGenericRepositoryTests: EFGenericTest
{
    private EFUnitOfWorkProvider dbContextProvider;
    public EFGenericRepositoryTests()
    {
        using var kaerMorhenDbContext = new KaerMorhenDBContext(_options);

        kaerMorhenDbContext.Add(new Contractor
            {Id = 1, Name = "FullName", Surname = "FullSurname", Contracts = new List<Contract>()});
        kaerMorhenDbContext.Add(new Contractor
            {Id = 2, Name = "NoSurname", Contracts = new List<Contract>()});

        kaerMorhenDbContext.SaveChanges();
        
        // var mockFactory = new Mock<DbContextFactory<KaerMorhenDBContext>>();
        // mockFactory.Setup(fact => fact.CreateDbContext()).Returns(new KaerMorhenDBContext(_options));
        // dbContextProvider = new EFUnitOfWorkProvider(mockFactory.Object);
    }

    [Fact]
    public async Task GetAllContractors_ReturnsTwo()
    {
        var mockFactory = new Mock<DbContextFactory<KaerMorhenDBContext>>();
        mockFactory.Setup(fact => fact.CreateDbContext()).Returns(new KaerMorhenDBContext(_options));
        var dbContextProvider = new EFUnitOfWorkProvider(mockFactory.Object);
        var repoUnderTest = new EFGenericRepository<Contractor>(dbContextProvider);

        var allContractors = await repoUnderTest.GetAll();
        
        Assert.True(allContractors.Count() == 2);
    }

    // [Fact]
    // public async Task InsertContractor_DbReturnsThree()
    // {
    //     var repoUnderTest = new EFGenericRepository<Contractor>(dbContextProvider);
    //     var contractor = new Contractor
    //         {Name = "NewContractorName", Surname = "NewContractorSurname", Contracts = new List<Contract>()};
    //
    //     await repoUnderTest.Insert(contractor);
    //     await dbContext.SaveChangesAsync();
    //
    //     var allContractors = await repoUnderTest.GetAll();
    //     Assert.True(allContractors.Count() == 3);
    // }

    // [Fact]
    // public async Task Get_ContractById_DbReturnsIdOne()
    // {
    //     var repoUnderTest = new EFGenericRepository<Contractor>(dbContextProvider);
    //
    //     var contractor = await repoUnderTest.GetById(1);
    //     
    //     Assert.True(contractor.Id == 1);
    //     Assert.Equal("FullName", contractor.Name);
    // }
    //
    // [Fact]
    // public async Task Delete_OneLeftAfterGet()
    // {
    //     var repoUnderTest = new EFGenericRepository<Contractor>(dbContextProvider);
    //
    //     await repoUnderTest.Delete(1);
    //     await dbContext.SaveChangesAsync();
    //     
    //     var allContractors = await repoUnderTest.GetAll();
    //     Assert.True(allContractors.Count() == 1);
    // }
    //
    // [Fact]
    // public async Task Update_Name_ReturnsUpdatedName()
    // {
    //     var repoUnderTest = new EFGenericRepository<Contractor>(dbContextProvider);
    //
    //     var contractor = await repoUnderTest.GetById(1);
    //     var oldName = contractor.Name;
    //     const string updatedName = "updatedName";
    //     contractor.Name = updatedName;
    //
    //     repoUnderTest.Update(contractor);
    //     await dbContext.SaveChangesAsync();
    //     
    //     var updatedContractor = await repoUnderTest.GetById(1);
    //     
    //     Assert.NotEqual(oldName, updatedContractor.Name);
    //     Assert.Equal(updatedName, updatedContractor.Name);
    // }
    //
    //
    // [Fact]
    // public async Task InsertContractViaContractor_DbReturnsOne()
    // {
    //     var repoUnderTestContractors = new EFGenericRepository<Contractor>(dbContextProvider);
    //
    //     var contractor = await repoUnderTestContractors.GetById(1);
    //     const string insertedContractName = "Find Yennefer";
    //     contractor.Contracts = new List<Contract>
    //     {
    //         new()
    //         {
    //             Name = insertedContractName, Description = "Look everywhere!", State = ContractState.Open,
    //             Location = "White Orchard", Deadline = new DateTime(2050, 12, 1)
    //         }
    //     };
    //
    //     repoUnderTestContractors.Update(contractor);
    //     await dbContext.SaveChangesAsync();
    //     
    //     var updatedContractor = await repoUnderTestContractors.GetById(1);
    //     
    //     Assert.True(updatedContractor.Contracts.Count == 1);
    //     Assert.Equal(updatedContractor.Contracts[0].Name, insertedContractName);
    //     
    //     var repoUnderTestContracts = new EFGenericRepository<Contract>(dbContextProvider);
    //     var returnedContracts = await repoUnderTestContracts.GetAll();
    //
    //     Assert.Equal(returnedContracts.First().Name, insertedContractName);
    // }
    //
    // // possible further test cases: lazy loading, nullables, navigation property...
    // public void Dispose()
    // {
    //     dbContext.Dispose();
    // }
}