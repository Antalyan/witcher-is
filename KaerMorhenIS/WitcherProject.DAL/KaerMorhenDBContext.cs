using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL.Data.Initializers;
using WitcherProject.DAL.Models;

namespace WitcherProject.DAL;

public class KaerMorhenDBContext : IdentityDbContext<Person, 
    Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Contractor> Contractors { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<ContractRequest> ContractRequests { get; set; }


    public KaerMorhenDBContext(DbContextOptions<KaerMorhenDBContext> options) : base(options)
    {
    }

    public KaerMorhenDBContext()
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        /* Setup the connection table */
        
        modelBuilder.HasDefaultSchema("witcher");
       
        modelBuilder.Entity<Contract>();
        modelBuilder.Entity<Contractor>();
        modelBuilder.Entity<ContractRequest>();
        modelBuilder.Entity<UserRole>();

        modelBuilder.Entity<Person>();
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        modelBuilder.Entity<UserRole>().HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
        modelBuilder.Entity<Role>();
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Seed();
    }
}