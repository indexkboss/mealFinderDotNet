using System;
using System.Collections.Generic;

namespace mealFinderDotNet.Models;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Prenom { get; set; }

    public string Email { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public DateTime DateInscription { get; set; }

    public string? PhotoProfil { get; set; }
}
