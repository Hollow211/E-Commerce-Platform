using Domain.Aggregates.ProductAggregate;
using Domain.Aggregates.ProductUnitAggregate;

namespace Domain.Aggregates.InvoiceAggregate;

public class InvoiceItem
{
    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal unitPrice { get; set; }

    public UnitType unitType { get; set; }

    public virtual Product Product { get; set; } = null!;

    public static InvoiceItem CreateInvoiceItem(Invoice Invoice, Product Product, int Quantity, ProductUnit productUnit)
    {
        if (Invoice.Id <= 0 || Product.Id <= 0 || Quantity <= 0)
            throw new ArgumentException("Invalid input values for creating an invoice item.");
        return new InvoiceItem
        {
            InvoiceId = Invoice.Id,
            ProductId = Product.Id,
            Quantity = Quantity,
            unitPrice = productUnit.UnitPrice,
        };
    }
}
