using Domain.AggregateNodes;

namespace Domain.Aggregates.InvoiceAggregate;

public class InvoiceItem
{
    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
