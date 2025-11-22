using System;
using System.ComponentModel.DataAnnotations;//c'est pour key and required

namespace MealFinder.Models
{
    public class Recherche
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MotCles { get; set; }
        public DateTime DateRecherche { get; set; } = DateTime.Now;
    }
}