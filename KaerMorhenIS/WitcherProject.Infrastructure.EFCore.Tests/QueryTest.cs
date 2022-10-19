using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.DAL.Models.Enums;
using WitcherProject.Infrastructure.EFCore.Query;
using Xunit;

namespace WitcherProject.Infrastructure.EFCore.Tests;

public class QueryTest
{

    private DbContextOptions<KaerMorhenDBContext> _options;
    

    public QueryTest()
    {
        var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        _options = new DbContextOptionsBuilder<KaerMorhenDBContext>()
                .UseInMemoryDatabase(databaseName: $"inMemory_{Guid.NewGuid()}")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

        using var kaerMorhenDbContext = new KaerMorhenDBContext(_options);
        
        
        kaerMorhenDbContext.Database.EnsureDeleted();
        // TODO: Find why this causes your code to crash!
        // System.ArgumentException: An item with the same key has already been added. Key: 1
        // kaerMorhenDbContext.Database.EnsureCreated();
        
        kaerMorhenDbContext.Contractors.Add(new Contractor() { Id = 1, Name = "Odolan", Surname = "White" });
        kaerMorhenDbContext.Contractors.Add(new Contractor() { Id = 2, Name = "Yontek", Surname = "" });
        kaerMorhenDbContext.Contractors.Add(new Contractor() { Id = 3, Name = "Holy", Surname = "Harry" });
        kaerMorhenDbContext.Contractors.Add(new Contractor() { Id = 4, Name = "Homer", Surname = "Kimchi" });
        kaerMorhenDbContext.Contractors.Add(new Contractor() { Id = 5, Name = "Hilde", Surname = "Teddings" });

        kaerMorhenDbContext.Contracts.Add(
            new Contract()
            {
                Id = 1,
                Name = "Devil by the Well",
                Description = "Slay the bitch - Odolan",
                State = ContractState.Accepted,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2022, 10, 1),
                ContractorId = 1,
                Deadline = new DateTime(2022, 11, 1),
                Location = "White Orchard",
                Person = null 
            });
        kaerMorhenDbContext.Contracts.Add(
            new Contract()
            {
                Id = 2,
                Name = "Jenny o' the Woods",
                Description = "Beware the night - Bolko",
                State = ContractState.Approved,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2022, 10, 2),
                ContractorId = 1,
                Deadline = new DateTime(2022, 10, 20),
                Location = "Midcopse",
                Person = null 
            });
        kaerMorhenDbContext.Contracts.Add(
            new Contract()
            {
                Id = 3,
                Name = "The Beast of Honorton",
                Description = "They eated dead, than my wife and children, kill them please - Holy",
                State = ContractState.Accepted,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2022, 10, 3),
                ContractorId = 3,
                Deadline = new DateTime(2022, 11, 3),
                Location = "Honorton",
                Person = null 
            });
        
        kaerMorhenDbContext.SaveChanges();
    }
    
    [Theory]
    [InlineData(1, "Odolan")]
    [InlineData(2, "Yontek")]
    public async Task Where_IdName_ReturnsSameContractor(int expectedId, string expectedName)
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        
        var query = new EFQuery<Contractor>(dbContext)
            .Filter(res => res.Id == expectedId && res.Name == expectedName);

        var queryResult = await query.ExecuteAsync();
        
        Assert.NotEmpty(queryResult);
        Assert.True(queryResult.First().Id == expectedId  && queryResult.First().Name == expectedName);
    }
    
    [Fact]
    public async Task Where_NameContains_ReturnSameContractor()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        
        var query = new EFQuery<Contractor>(dbContext)
            .Filter(res => res.Name.Contains("ol"));

        var queryResult = await query.ExecuteAsync();
        
        Assert.NotEmpty(queryResult);
        Assert.Equal(2, queryResult.Count());
        Assert.DoesNotContain(new Contractor() { Id = 3, Name = "Holy", Surname = "Harry" }, queryResult);
    }
    
    [Fact]
    public async Task OrderBy_NameDescending_ReturnsDescendingByName()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        
        var query = new EFQuery<Contractor>(dbContext)
            .OrderBy(cond => cond.Name, ascending: false);

        var queryResult = await query.ExecuteAsync();
        
        Assert.NotEmpty(queryResult);
        Assert.Equal(2, queryResult.First().Id);
        Assert.Equal("Yontek", queryResult.First().Name);
    } 
    
    [Fact]
    public async Task Where_ContractorId_OrderedByDeadline_ReturnsContractsOrdered()
    {
        using var dbContext = new KaerMorhenDBContext(_options);
        
        var query = new EFQuery<Contract>(dbContext)
            .Filter(cont => cont.ContractorId == 1)
            .OrderBy(cond => cond.Deadline);
    
        var queryResult = await query.ExecuteAsync();
        
        Assert.NotEmpty(queryResult);
        Assert.Equal(new DateTime(2022, 10, 20), queryResult.First().Deadline);
        Assert.Equal(2, queryResult.Count());
    } 
    
    [Fact]
    public async Task Page_Second_ReturnsOneContractor()
    {
        using var dbContext = new KaerMorhenDBContext(_options);

        var query = new EFQuery<Contractor>(dbContext)
            .Filter(cont => cont.Name.StartsWith('H'))
            .Page(2, 1);
    
        var queryResult = await query.ExecuteAsync();
        
        Assert.NotEmpty(queryResult);
        Assert.Equal("Homer", queryResult.First().Name);
    } 
    
}