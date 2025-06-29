using Domain.AggregateNodes;
using Domain.Aggregates.ProductAggregate;
using Domain.POCO;
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

    public static Result<Invoice> CreateInvoice(Customer customer, 
        DateTime issueDate,
        List<InvoiceDetailPOCO> soldPorductsPoco)
    {
        // Validation
        if (CheckValidation(customer, issueDate, soldPorductsPoco) == false)
            return Result.Fail("Invalid or missing inputs");

        // Create invoice
        Invoice newInvoice = new Invoice();

        // Create invoice items
        var items = new List<InvoiceItem>();
        foreach (var entry in soldPorductsPoco) // Loop on each product
        {
            var unitPrice = entry.Product.Units.FirstOrDefault(x => x.UnitId == entry.Unit.Id)!.UnitPrice;
            InvoiceItem item = InvoiceItem.CreateInvoiceItem(entry.Product.Id, entry.Quantity, unitPrice, entry.Unit.Type);
            newInvoice.TotalAmount += (unitPrice * entry.Quantity);
            newInvoice.Items.Add(item);
        }

        // Create invoice
        newInvoice.IssueDate = issueDate;
        newInvoice.CustomerId = customer.Id;
        newInvoice.isPaid = false;

        return Result.Ok(newInvoice);
    }

    private static bool CheckValidation(Customer customer, DateTime issueDate, List<InvoiceDetailPOCO> products)
    {
        if (customer == null)
            return false;
        if (products.Count == 0)
            return false;
        return true;
    }
}
