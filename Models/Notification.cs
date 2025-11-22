using System;
using System.ComponentModel.DataAnnotations;

namespace MealFinder.Models
{
    public class Notification
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Contenu { get; set; }
        [Required]
        public DateTime DateEnvoi { get; set; }
        public bool EstLue { get; set; } = false;
    }
}