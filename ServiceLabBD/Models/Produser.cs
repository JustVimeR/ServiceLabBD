using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Produser
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Adress { get; set; }

    public virtual ICollection<CarPart> CarParts { get; set; } = new List<CarPart>();
}
