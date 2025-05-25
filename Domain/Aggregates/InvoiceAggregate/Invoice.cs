using Domain.AggregateNodes;
using Domain.Aggregates.ProductAggregate;
using Domain.Shared;
using FluentResults;
namespace Domain.Aggregates.InvoiceAggregate;

public class Invoice : Entity<int>
{
    public int CustomerId { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public bool isPaid { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

    public static Result<Invoice> CreateInvoice(int customerId, DateTime issueDate, decimal totalAmount, List<Product> products)
    {
        // Validation
        if (CheckValidation(customerId, issueDate, totalAmount,products) == false)
        {
            return Result.Fail("Invalid or missing inputs");
        }

        // Create invoice id
        var invoiceId = Random.Shared.Next(1, 1000);

        // Create invoice items
        var items = new List<InvoiceItem>();
        /*foreach (var product in products)
        {
            InvoiceItem item = InvoiceItem.CreateInvoiceItem(invoiceId, product.Key.Id, product.Value, false, product.); // Value is quantity
            // TODO: Validation
            items.Add(item);
        }*/

        // Create invoice
        Invoice newInvoice =  new Invoice
        {
            Id = invoiceId,
            CustomerId = customerId,
            IssueDate = issueDate,
            TotalAmount = totalAmount,
            isPaid = false,
            Items = items,
        };

        return Result.Ok(newInvoice);
    }

    private static bool CheckValidation(int customerId, DateTime issueDate, decimal totalAmount, List<Product> products)
    {
        if (customerId <= 0)
            return false;
        if (totalAmount <= 0)
            return false;
        if (products.Count == 0)
            return false;
        return true;
    }
}
