// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using WitcherProject.DAL;
// using WitcherProject.DAL.Models;
// using WitcherProject.Infrastructure.EFCore.Repository;
// using Xunit;
//
// namespace WitcherProject.Infrastructure.EFCore.Tests;
//
// public class UnitOfWorkContractsTests: EFGenericTest
// {
//     public UnitOfWorkContractsTests()
//     {
//         using var kaerMorhenDbContext = new KaerMorhenDBContext(_options);
//         
//         kaerMorhenDbContext.Add(new Contractor
//             {Id = 1, Name = "FullName", Surname = "FullSurname", Contracts = new List<Contract>()});
//
//         kaerMorhenDbContext.SaveChanges();
//     }
//
//     [Fact]
//     public async Task TwoContractorsAdded_DbReturnsThreeObjects()
//     {
//         using var dbContext = new KaerMorhenDBContext(_options);
//         var uowUnderTest = new UnitOfWorkContracts(dbContext, new EFGenericRepository<Person>(dbContext), 
//             new EFGenericRepository<Contract>(dbContext), new EFGenericRepository<Contractor>(dbContext),
//             new EFGenericRepository<ContractRequest>(dbContext));
//         var contractor1 = new Contractor
//             {Name = "NewContractorName1", Surname = "NewContractorSurname1", Contracts = new List<Contract>()};
//         var contractor2 = new Contractor
//             {Name = "NewContractorName2", Surname = "NewContractorSurname2", Contracts = new List<Contract>()};
//
//         await uowUnderTest.ContractorRepository.Insert(contractor1);
//         var originalContractor = await uowUnderTest.ContractorRepository.GetById(1);
//         await uowUnderTest.ContractorRepository.Insert(contractor2);
//         await uowUnderTest.CommitAsync();
//
//         var allContractors = await uowUnderTest.ContractorRepository.GetAll();
//         Assert.True(allContractors.Count() == 3);
//         Assert.Equal("FullName", originalContractor.Name);
//     }
//
//     [Fact]
//     public async Task InvalidContractor_ShouldNotInsert()
//     {
//         using var dbContext = new KaerMorhenDBContext(_options);
//         var uowUnderTest = new UnitOfWorkContracts(dbContext, new EFGenericRepository<Person>(dbContext), 
//             new EFGenericRepository<Contract>(dbContext), new EFGenericRepository<Contractor>(dbContext),
//             new EFGenericRepository<ContractRequest>(dbContext));
//         var validContractor = new Contractor
//             {Name = "NewContractorName1", Surname = "NewContractorSurname1", Contracts = new List<Contract>()};
//         var invalidContractor = new Contractor
//             {Id = 1, Name = "NewContractorName2", Surname = "NewContractorSurname2", Contracts = new List<Contract>()};
//
//         await uowUnderTest.ContractorRepository.Insert(validContractor);
//         var act = () =>  uowUnderTest.ContractorRepository.Insert(invalidContractor);   // based on: https://stackoverflow.com/a/45017575
//         
//         var allContractors = await uowUnderTest.ContractorRepository.GetAll();
//         await Assert.ThrowsAsync<InvalidOperationException>(act);
//         Assert.True(allContractors.Count() == 1);
//     }
// }