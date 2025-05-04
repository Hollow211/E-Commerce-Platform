using Domain.Aggregates.InvoiceAggregate;
using Domain.Shared;
using System;
using System.Collections.Generic;

namespace Domain.AggregateNodes;

public class Product: Entity<int>
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
