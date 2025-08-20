using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL.Models;
using WitcherProject.Shared;
using WitcherProject.Shared.Enums;

namespace WitcherProject.Infrastructure.EFCore.Data.Initializers;

public static class DataInitializer
{
    public static async Task SeedAsync(this KaerMorhenDBContext context, CancellationToken cancellationToken = default)
    {
        var nowUtc = DateTime.UtcNow;

        // Seed Persons
        if (!await context.Persons.AnyAsync(cancellationToken))
        {
            var geralt = new Person
            {
                Id = 1,
                Name = "Geralt",
                Surname = "of Rivia",
                Cv = "Butcher of Blaviken",
                UserName = "wolf",
                IsActive = true,
                Birthdate = nowUtc,
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
                Birthdate = nowUtc,
                NormalizedUserName = "VESEMIR",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var lambert = new Person
            {
                Id = 3,
                Name = "Lambert",
                Surname = "",
                Cv = "What a prick",
                UserName = "lambert",
                IsActive = true,
                Birthdate = nowUtc,
                NormalizedUserName = "LAMBERT",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Hash passwords at runtime
            var hasher = new PasswordHasher<Person>();
            geralt.PasswordHash = hasher.HashPassword(geralt, "GeraltOfRevia123*");
            vesemir.PasswordHash = hasher.HashPassword(vesemir, "OldWolf1*");
            lambert.PasswordHash = hasher.HashPassword(lambert, "12HandsomeLamb*");

            await context.Persons.AddRangeAsync(geralt, vesemir, lambert);
        }

        // Seed Contractors
        if (!await context.Contractors.AnyAsync(cancellationToken))
        {
            var odolan = new Contractor { Id = 1, Name = "Odolan", Surname = "White" };
            await context.Contractors.AddAsync(odolan, cancellationToken);
        }

        // Seed Contracts
        if (!await context.Contracts.AnyAsync(cancellationToken))
        {
            var noonWraithContract = new Contract
            {
                Id = 1,
                Name = "Devil by the Well",
                Description = "Slay the bitch - Odolan",
                State = ContractState.Open,
                StartDate = nowUtc,
                EndDate = nowUtc.AddMonths(12),
                ContractorId = 1,
                Deadline = nowUtc.AddMonths(13),
                Location = "White Orchard",
                Person = null
            };
            await context.Contracts.AddAsync(noonWraithContract, cancellationToken);
        }

        // Seed Roles
        if (!await context.Roles.AnyAsync(cancellationToken))
        {
            var roles = new[]
            {
                new Role { Id = 1, Name = RoleNames.Admin, NormalizedName = RoleNames.Admin.ToUpper(CultureInfo.InvariantCulture)},
                new Role { Id = 2, Name = RoleNames.UserManager, NormalizedName = RoleNames.UserManager.ToUpper(CultureInfo.InvariantCulture)},
                new Role { Id = 3, Name = RoleNames.Witcher, NormalizedName = RoleNames.Witcher.ToUpper(CultureInfo.InvariantCulture)},
                new Role { Id = 4, Name = RoleNames.ContractManager, NormalizedName = RoleNames.ContractManager.ToUpper(CultureInfo.InvariantCulture)},
            };
            await context.Roles.AddRangeAsync(roles, cancellationToken);
        }

        // Seed UserRoles
        if (!await context.UserRoles.AnyAsync(cancellationToken))
        {
            var userRoles = new[]
            {
                new UserRole { RoleId = 1, UserId = 1 },
                new UserRole { RoleId = 2, UserId = 1 },
                new UserRole { RoleId = 3, UserId = 1 },
                new UserRole { RoleId = 4, UserId = 1 },
                new UserRole { RoleId = 3, UserId = 2 },
                new UserRole { RoleId = 4, UserId = 2 },
                new UserRole { RoleId = 3, UserId = 3 }
            };
            await context.UserRoles.AddRangeAsync(userRoles, cancellationToken);
        }

        // Seed ContractRequests
        if (!await context.ContractRequests.AnyAsync(cancellationToken))
        {
            var request = new ContractRequest
            {
                Id = 1,
                CreatedOn = nowUtc,
                PersonId = 1,
                ContractId = 1,
                State = ContractRequestState.Approved,
                Text = "I need money"
            };
            await context.ContractRequests.AddAsync(request, cancellationToken);
        }

        // Save everything at once
        await context.SaveChangesAsync(cancellationToken);
    }
}
