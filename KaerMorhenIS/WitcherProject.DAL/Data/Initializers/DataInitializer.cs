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
            Login = "wolf",
            PasswordHash = "1111",
            IsActive = true,
            Birthdate = DateTime.Now
        };
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

        var masterRole = new Role { Id = 1, RoleName = "Master" };


        modelBuilder.Entity<Person>()
            .HasData(geralt);

        modelBuilder.Entity<Contractor>()
            .HasData(odolan);

        modelBuilder.Entity<Contract>()
            .HasData(noonWraithContract);

        modelBuilder.Entity<Role>()
            .HasData(masterRole);

        modelBuilder.Entity<RoleToPerson>()
            .HasData(new RoleToPerson { Id = 1, RoleId = masterRole.Id, PersonId = geralt.Id });

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