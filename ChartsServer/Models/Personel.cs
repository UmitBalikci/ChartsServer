using System;
using System.Collections.Generic;

namespace ChartsServer.Models;

public partial class Personel
{
    public int PersonelId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
