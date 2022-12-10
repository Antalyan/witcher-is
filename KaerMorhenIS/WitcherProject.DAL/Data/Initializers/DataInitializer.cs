using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL.Models;
using WitcherProject.Shared.Enums;

namespace WitcherProject.DAL.Data.Initializers;

public static class DbInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var geralt = new Person
        {
            Id = 1,
            Name = "Geralt",
            Surname = "ofRivia",
            Cv = "ButcherofBlaviken",
            UserName = "wolf",
            IsActive = true,
            Birthdate = DateTime.Now,
            NormalizedUserName = "WOLF",
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        geralt.PasswordHash = new PasswordHasher<Person>().HashPassword(geralt, "GeraltOfRevia123");
        var odolan = new Contractor() { Id = 1, Name = "Odolan", Surname = "White" };
        var noonWraithContract = new Contract()
        {
            Id = 1,
            Name = "Devil by the Well",
            Description = "Slay the bitch - Odolan",
            State = ContractState.Open,
            StartDate = DateTime.Now,
            EndDate = new DateTime(2022, 10, 1),
            ContractorId = odolan.Id,
            Deadline = new DateTime(2022, 11, 1),
            Location = "White Orchard",
            Person = null
        };

        var userManager = new Role() { Id = 1, Name = "UserManager" };

        
       
        modelBuilder.Entity<Person>()
            .HasData(geralt);

        modelBuilder.Entity<Contractor>()
            .HasData(odolan);

        modelBuilder.Entity<Contract>()
            .HasData(noonWraithContract);

        modelBuilder.Entity<Role>()
            .HasData(userManager);

        modelBuilder.Entity<UserRole>()
            .HasData(new UserRole()
            {
                RoleId = userManager.Id,
                UserId = geralt.Id
            });
        
        modelBuilder.Entity<ContractRequest>()
            .HasData(new ContractRequest
            {
                Id = 1,
                CreatedOn = DateTime.Now,
                PersonId = geralt.Id,
                ContractId = noonWraithContract.Id,
                State = ContractRequestState.Accepted,
                Text = "I need money"
            });
    }
}