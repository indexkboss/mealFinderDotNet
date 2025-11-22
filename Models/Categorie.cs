using System.ComponentModel.DataAnnotations;//c'est pour key and required

namespace MealFinder.Models
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
    }
}