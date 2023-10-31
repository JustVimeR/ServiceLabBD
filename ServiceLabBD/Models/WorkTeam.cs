using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class WorkTeam
{
    public int Id { get; set; }

    public int MasterId { get; set; }

    public int MechanicId { get; set; }

    public virtual Master Master { get; set; }

    public virtual Mechanic Mechanic { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
