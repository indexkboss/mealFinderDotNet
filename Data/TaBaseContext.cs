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

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<Favori> Favoris { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Recette> Recettes { get; set; }

    public virtual DbSet<Recherch> Recherches { get; set; }

    public virtual DbSet<Statistique> Statistiques { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
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
