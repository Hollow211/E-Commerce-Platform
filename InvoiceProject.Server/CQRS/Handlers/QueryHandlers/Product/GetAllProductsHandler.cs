using Application.CQRS.Queries.GetAllProduct;
using Domain.Aggregates.ProductAggregate;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.QueryHandlers.GetAllProductQueryHandler
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return (List<Product>)await _productRepository.GetAllProducts();
        }
    }
}
