using Domain.Aggregates;
using Domain.Aggregates.ProductAggregate;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Queries.GetAllProduct
{
    public record GetAllProducts : IRequest<List<Product>>
    {

    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProducts, List<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<List<Product>> Handle(GetAllProducts request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllProducts();
        }
    }
}
