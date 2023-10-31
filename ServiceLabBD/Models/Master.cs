using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Master
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public int Experience { get; set; }

    public string QualificationLevel { get; set; }

    public virtual ICollection<WorkTeam> WorkTeams { get; set; } = new List<WorkTeam>();
}
