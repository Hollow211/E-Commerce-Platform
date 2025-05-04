using Application.CQRS.Queries.Responses;
using Domain.AggregateNodes;
using MediatR;

namespace Application.CQRS.Queries.Requests
{
    public record GetCustomerByIdQuery : IRequest<GetCustomerResponse>
    {
        public required int id { get; set; }
    }
}
