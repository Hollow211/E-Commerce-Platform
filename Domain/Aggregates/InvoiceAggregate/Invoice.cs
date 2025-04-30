using Domain.AggregateNodes;
using Domain.Shared;
namespace Domain.Aggregates.InvoiceAggregate;

public class Invoice : Entity<int>
{

    public int InvoiceNumber { get; set; }

    public int Customer { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public bool Due { get; set; }

    public virtual Customer CustomerNavigation { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceProducts { get; set; } = new List<InvoiceItem>();
}
