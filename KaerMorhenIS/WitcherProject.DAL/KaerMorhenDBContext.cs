using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL.Data.Initializers;
using WitcherProject.DAL.Models;

namespace WitcherProject.DAL;

public class KaerMorhenDBContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Contractor> Contractors { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleToPerson> RoleToPersons { get; set; }
    public DbSet<ContractRequest> ContractRequests { get; set; }


    public KaerMorhenDBContext(DbContextOptions<KaerMorhenDBContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Setup the connection table */
        modelBuilder.Entity<Person>();
        modelBuilder.Entity<Contract>();
        modelBuilder.Entity<Contractor>();
        modelBuilder.Entity<ContractRequest>();
        modelBuilder.Entity<Role>();
        modelBuilder.Entity<RoleToPerson>();

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}