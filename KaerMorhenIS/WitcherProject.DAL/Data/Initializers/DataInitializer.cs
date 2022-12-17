using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL.Models;
using WitcherProject.Shared;
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
        
        var vesemir = new Person
        {
            Id = 2,
            Name = "Vesemir",
            Surname = "",
            Cv = "Old coot",
            UserName = "vesemir",
            IsActive = true,
            Birthdate = DateTime.Now,
            NormalizedUserName = "VESEMIR",
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var lambert = new Person
        {
            Id = 3,
            Name = "lambert",
            Surname = "",
            Cv = "What a prick",
            UserName = "lambert",
            IsActive = true,
            Birthdate = DateTime.Now,
            NormalizedUserName = "LAMBERT",
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        // Create default passwords for witchers
        
        geralt.PasswordHash = new PasswordHasher<Person>().HashPassword(geralt, "GeraltOfRevia123*");
        vesemir.PasswordHash = new PasswordHasher<Person>().HashPassword(geralt, "OldWolf1*");
        lambert.PasswordHash = new PasswordHasher<Person>().HashPassword(geralt, "12HandsomeLamb*");
        
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
        
        modelBuilder.Entity<Person>()
            .HasData(geralt, vesemir, lambert);

        modelBuilder.Entity<Contractor>()
            .HasData(odolan);

        modelBuilder.Entity<Contract>()
            .HasData(noonWraithContract);

        modelBuilder.Entity<Role>().HasData
        (
            new Role() { Id = 1, Name = RoleNames.Admin, NormalizedName = RoleNames.Admin.ToUpper(CultureInfo.InvariantCulture)},
            new Role() { Id = 2, Name = RoleNames.UserManager, NormalizedName = RoleNames.UserManager.ToUpper(CultureInfo.InvariantCulture)},
            new Role() { Id = 3, Name = RoleNames.Witcher, NormalizedName = RoleNames.Witcher.ToUpper(CultureInfo.InvariantCulture)},
            new Role() { Id = 4, Name = RoleNames.ContractManager, NormalizedName = RoleNames.ContractManager.ToUpper(CultureInfo.InvariantCulture)}
        );
        
        modelBuilder.Entity<UserRole>().HasData
        (
            new UserRole() { RoleId = 1, UserId = geralt.Id },
            new UserRole() { RoleId = 2, UserId = geralt.Id },
            new UserRole() { RoleId = 3, UserId = geralt.Id },
            new UserRole() { RoleId = 4, UserId = geralt.Id },
            new UserRole() { RoleId = 3, UserId = vesemir.Id },
            new UserRole() { RoleId = 4, UserId = vesemir.Id },
            new UserRole() { RoleId = 3, UserId = lambert.Id }
        );
        
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