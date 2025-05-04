using Domain.Aggregates.InvoiceAggregate;
using Domain.Shared;
using System;
using System.Collections.Generic;

namespace Domain.AggregateNodes;

public class Customer : Entity<int>
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public bool CanDeleteCustomer(int customerId)
    {
        bool isThereUnPaid = Invoices.Any(inv => !inv.isPaid);
        return !isThereUnPaid;
    }
}
