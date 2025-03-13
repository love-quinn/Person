using System;
using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Data;

public class PersonContext : DbContext
{
    public DbSet<PersonModel> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonModel>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<PersonModel>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=person.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}
