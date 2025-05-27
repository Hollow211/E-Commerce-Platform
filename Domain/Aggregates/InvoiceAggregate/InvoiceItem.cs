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

    public static InvoiceItem CreateInvoiceItem(int productId, int Quantity, decimal unitPrice, UnitType unitType)
    {
        return new InvoiceItem
        {
            ProductId = productId,
            Quantity = Quantity,
            unitPrice = unitPrice,
            unitType = unitType,
        };
    }
}
