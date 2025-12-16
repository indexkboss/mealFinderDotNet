using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace mealFinderDotNet.Models;

public partial class Favori
{
    [Key]
    public int Id { get; set; }

    public int IdRecetteAPI { get; set; }
    public int UtilisateurId { get; set; }

}
