using System;
using System.Collections.Generic;

namespace mealFinderDotNet.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string Contenu { get; set; } = null!;

    public DateTime DateEnvoi { get; set; }

    public bool EstLue { get; set; }
}
