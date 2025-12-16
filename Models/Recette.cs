using System;
using System.Collections.Generic;

namespace mealFinderDotNet.Models;

public partial class Recette
{
    public int Id { get; set; }

    public string Titre { get; set; }

    public string Image { get; set; }

    public int ReadyInMinutes { get; set; }

    public int Servings { get; set; }

    public List<string> Ingredients { get; set; }

    public string Instructions { get; set; } 
}
