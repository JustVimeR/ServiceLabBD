using System;
using System.Collections.Generic;

namespace ServiceLabBD;

public partial class CarPart
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int ProduserId { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public int StocksId { get; set; }

    public virtual Produser Produser { get; set; }

    public virtual Stock Stocks { get; set; }
}
