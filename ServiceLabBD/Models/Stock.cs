using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Stock
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public virtual ICollection<CarPart> CarParts { get; set; } = new List<CarPart>();
}
