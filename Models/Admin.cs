using System.ComponentModel.DataAnnotations;

namespace MealFinder.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string MotDePasse { get; set; }
    }
}