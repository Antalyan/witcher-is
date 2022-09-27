using Microsoft.EntityFrameworkCore;
using WitcherProject.Data.Models;

namespace WitcherProject.Data;

public static class PersonInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var jozko = new Person
        {
            Id = 1, Name = "Jozko", Surname = "Mrkvicka", Cv = "hrotic", Login = "makac", PasswordHash = "1111",
            IsActive = true, Birthdate = DateTime.Now
        };

        modelBuilder.Entity<Person>().HasData(jozko);
    }
}