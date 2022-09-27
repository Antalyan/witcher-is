using Microsoft.EntityFrameworkCore;
using WitcherProject.Data.Models;

namespace WitcherProject.Data;

public class KaerMorhenDBContext : DbContext
{
    public  DbSet<Person> Persons { get; set; }

    public KaerMorhenDBContext(DbContextOptions<KaerMorhenDBContext> options): base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Setup the connection table */
        modelBuilder.Entity<Person>();

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}