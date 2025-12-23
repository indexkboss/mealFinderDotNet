namespace mealFinderDotNet.Models
{
    public class RecetteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }           // Nom de la recette
        public string Image { get; set; }           // URL de l'image
        public int ReadyInMinutes { get; set; }     // Temps de préparation
        public int Servings { get; set; }           // Nombre de portions
        public string Instructions { get; set; }    // Instructions

        public List<string> Ingredients { get; set; } = new List<string>(); // Liste des ingrédients
    }
}
