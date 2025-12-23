using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mealFinderDotNet.Models
{
    public class Recette
    {
        [Key]
        public int Id { get; set; } // Id local DB

        [Required]
        public int ApiId { get; set; } // ✅ NON nullable

        [Required]
        public string Title { get; set; }

        public string Image { get; set; }
        public int ReadyInMinutes { get; set; }
        public int Servings { get; set; }
        public string Instructions { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
            = new List<Ingredient>();
    }
}
