using Domain.Aggregates.ProductAggregate;
using MediatR;

namespace Application.CQRS.Queries.GetAllProduct
{
    public record GetAllProductsQuery : IRequest<List<Product>>
    {

    }
}
