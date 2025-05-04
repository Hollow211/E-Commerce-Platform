using Domain.AggregateNodes;
using Domain.Aggregates.InvoiceAggregate;

namespace Application.CQRS.Queries.Responses
{
    public record GetCustomerInvoicesResponse
    {
        public required bool isSuccess { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
}
