using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class DiscountCard
{
    public int Id { get; set; }

    public int BonusesTotal { get; set; }

    public int DiscountTotal { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
