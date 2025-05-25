using Domain.Aggregates.InvoiceAggregate;
using MediatR;

namespace Application.CQRS.Queries.Customer
{
    public record GetCustomerInvoices: IRequest<GetCustomerInvoicesResponse>
    {
        public required int id { get; set; }
    }
}
