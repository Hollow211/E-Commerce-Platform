using Application.CQRS.Queries.GetCustomerResponseNameSpace;
using Domain.AggregateNodes;
using MediatR;

namespace Application.CQRS.Queries.Customer
{
    public record GetCustomerByIdQuery : IRequest<GetCustomerResponse>
    {
        public required int id { get; set; }
    }
}
