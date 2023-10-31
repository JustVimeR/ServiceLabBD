using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class WorkTime
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
