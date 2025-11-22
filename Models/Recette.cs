using System.ComponentModel.DataAnnotations;//c'est pour key and required

namespace MealFinder.Models
{
    public class Recette
    {   
        [Key] // PK = clé primaire de la table dans la base de données
        public int Id { get; set; }
        [Required] // Required = le champ doit obligatoirement avoir une valeur
        public string Titre { get; set; }
        public int TempsPreparation { get; set; } // Durée en minutes
        public int NombrePortions { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>(); // Liste d'ingrédients (sera stockée en JSON dans la base)
        public string Instructions { get; set; }
        public string Image { get; set; } // URL ou chemin vers l'image
        public string SourceAPI { get; set; } // Si la recette vient d'une API externe (Spoonacular)
    }
}