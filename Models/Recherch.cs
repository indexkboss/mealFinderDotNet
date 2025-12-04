using System;
using System.Collections.Generic;

namespace mealFinderDotNet.Models;

public partial class Recherch
{
    public int Id { get; set; }

    public string MotCles { get; set; } = null!;

    public DateTime DateRecherche { get; set; }
}
