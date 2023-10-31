using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Service
{
    public int Id { get; set; }

    public int WorkTypeId { get; set; }

    public int ClientId { get; set; }

    public int AutoId { get; set; }

    public int ManagerId { get; set; }

    public int WorkTeamId { get; set; }

    public int EquipmentId { get; set; }

    public int WorkTimeId { get; set; }

    public virtual Auto Auto { get; set; }

    public virtual Client Client { get; set; }

    public virtual Equipment Equipment { get; set; }

    public virtual Manager Manager { get; set; }

    public virtual WorkTeam WorkTeam { get; set; }

    public virtual WorkTime WorkTime { get; set; }

    public virtual WorkType WorkType { get; set; }
}
