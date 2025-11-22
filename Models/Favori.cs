using System;
using System.ComponentModel.DataAnnotations;//c'est pour key and required

namespace MealFinder.Models
{
    public class Favori
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateAjout { get; set; } = DateTime.Now;
    }
}