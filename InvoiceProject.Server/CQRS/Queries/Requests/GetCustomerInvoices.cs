using Application.CQRS.Queries.Responses;
using Domain.Aggregates.InvoiceAggregate;
using MediatR;

namespace Application.CQRS.Queries.Requests
{
    public record GetCustomerInvoices: IRequest<GetCustomerInvoicesResponse>
    {
        public required int id { get; set; }
    }
}
