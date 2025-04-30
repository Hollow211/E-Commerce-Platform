using Domain.Aggregates.InvoiceAggregate;
using System;
using System.Collections.Generic;

namespace Domain.AggregateNodes;

public class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal ProductPrice { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceProducts { get; set; } = new List<InvoiceItem>();
}
