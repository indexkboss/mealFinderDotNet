using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mealFinderDotNet.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int RecetteId { get; set; }
        public virtual Recette Recette { get; set; }
    }
}
