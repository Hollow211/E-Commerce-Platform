using Domain.AggregateNodes;
using Domain.Shared;
namespace Domain.Aggregates.InvoiceAggregate;

public class Invoice : Entity<int>
{

    public int InvoiceNumber { get; set; }

    public int CustomerId { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public bool isPaid { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
}
