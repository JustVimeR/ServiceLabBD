using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class Client
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public int DiscountCardId { get; set; }

    public virtual DiscountCard DiscountCard { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
