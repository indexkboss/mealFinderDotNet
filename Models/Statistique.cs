using System;
using System.Collections.Generic;

namespace mealFinderDotNet.Models;

public partial class Statistique
{
    public int Id { get; set; }

    public int TotalUtilisateurs { get; set; }

    public int TotalRecherches { get; set; }

    public int TotalFavoris { get; set; }
}
