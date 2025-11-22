using System;
using System.ComponentModel.DataAnnotations;

namespace MealFinder.Models
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string? Prenom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        public string MotDePasse { get; set; }
        public DateTime DateInscription { get; set; } = DateTime.Now;
        public string? PhotoProfil { get; set; } //Certaines propriétés doivent être nullable (string?) pour éviter les erreurs EF
    }
}
