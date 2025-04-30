using Domain.Aggregates.InvoiceAggregate;
using Domain.Shared;
using System;
using System.Collections.Generic;

namespace Domain.AggregateNodes;

public class Customer : Entity<int>
{
    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string CustomerAddress { get; set; } = null!;

    public string CustomerPhoneNumber { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public bool CanDeleteCustomer(int customerId)
    {
        bool isThereDue = Invoices.Any(inv => inv.Due);
        return !isThereDue;
    }
}
