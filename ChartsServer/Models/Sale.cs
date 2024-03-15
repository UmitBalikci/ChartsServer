using System;
using System.Collections.Generic;

namespace ChartsServer.Models;

public partial class Sale
{
    public int SalesId { get; set; }

    public int? PersonelId { get; set; }

    public int? Price { get; set; }

    public virtual Personel? Personel { get; set; }
}
