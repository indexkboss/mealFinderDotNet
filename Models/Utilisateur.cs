using System;
using System.ComponentModel.DataAnnotations;

namespace mealFinderDotNet.Models
{
    public partial class Utilisateur
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirebaseId { get; set; } // pour lier l'utilisateur Firebase
    }
}
