using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }

    public int WarrantyDays { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
