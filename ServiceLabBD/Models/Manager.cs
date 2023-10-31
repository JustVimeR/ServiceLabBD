using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Manager
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public int Salary { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
