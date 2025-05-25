using Domain.AggregateNodes;

namespace Application.CQRS.Queries.GetCustomerResponseNameSpace
{
    public record GetCustomerResponse
    {
        public required bool isSuccess { get; set; }

        public Domain.AggregateNodes.Customer? customer { get; set; }
    }
}
