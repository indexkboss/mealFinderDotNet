using System.ComponentModel.DataAnnotations;

namespace MealFinder.Models
{
    public class Statistique
    {
        [Key]
        public int Id { get; set; }
        public int TotalUtilisateurs { get; set; }
        public int TotalRecherches { get; set; }
        public int TotalFavoris { get; set; }
    }
}