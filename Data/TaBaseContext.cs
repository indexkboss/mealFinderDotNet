using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using mealFinderDotNet.Models;

namespace mealFinderDotNet.Data;

public partial class TaBaseContext : DbContext
{
    public TaBaseContext()
    {
    }

    public TaBaseContext(DbContextOptions<TaBaseContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<Favori> Favoris { get; set; }



    public virtual DbSet<Recette> Recettes { get; set; }

  


    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MealFinderDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recette>(entity =>
        {
            entity.Property(e => e.SourceApi).HasColumnName("SourceAPI");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
