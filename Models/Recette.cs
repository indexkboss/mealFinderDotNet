using System;
using System.Collections.Generic;

namespace mealFinderDotNet.Models;

public partial class Recette
{
    public int Id { get; set; }

    public string Titre { get; set; } = null!;

    public int TempsPreparation { get; set; }

    public int NombrePortions { get; set; }

    public string Ingredients { get; set; } = null!;

    public string Instructions { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string SourceApi { get; set; } = null!;
}
