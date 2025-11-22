using Microsoft.EntityFrameworkCore;
using MealFinder.Models;

namespace mealFinderDotNet.Data

{
    public class MealFinderDbContext : DbContext
    {
        public MealFinderDbContext(DbContextOptions<MealFinderDbContext> options)
            : base(options)
        {
        }

        // DbSets pour tous tes modèles
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Favori> Favoris { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Recette> Recettes { get; set; }
        public DbSet<Recherche> Recherches { get; set; }
        public DbSet<Statistique> Statistiques { get; set; }
    }
}

//Le DbContext est le cœur d’Entity Framework.
//Il sert de pont entre tes modèles C# et la base de données.
// Explications :
//	DbContext: classe de base qui gère les opérations sur la base (CRUD, migrations…)
//	DbSet<Utilisateur> Utilisateurs : chaque DbSet correspond à une table dans la base
//	Le constructeur avec DbContextOptions permet de configurer la connexion à SQL Server dans Program.cs
