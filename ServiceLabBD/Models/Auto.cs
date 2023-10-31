using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Auto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Model { get; set; }

    public int Year { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
