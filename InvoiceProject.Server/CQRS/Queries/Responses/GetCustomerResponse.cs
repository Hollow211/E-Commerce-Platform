using Domain.AggregateNodes;

namespace Application.CQRS.Queries.Responses
{
    public record GetCustomerResponse
    {
        public required bool isSuccess { get; set; }

        public Customer? customer { get; set; }
    }
}
