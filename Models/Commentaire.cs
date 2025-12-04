using System;
using System.Collections.Generic;

namespace mealFinderDotNet.Models;

public partial class Commentaire
{
    public int Id { get; set; }

    public string Contenu { get; set; } = null!;

    public DateTime DateCommentaire { get; set; }
}
