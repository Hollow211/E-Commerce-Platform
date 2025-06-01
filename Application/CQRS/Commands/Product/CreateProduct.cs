using Application.DTOs;
using Domain.Aggregates.ProductAggregate;
using Domain.Aggregates.ProductUnitAggregate;
using Domain.POCO;
using Application.Interfaces;
using FluentResults;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Commands.ProductCommands
{
    public class CreateProductResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class CreateProduct: IRequest<CreateProductResponse>
    {
        public required string name { get; set; }

        public List<UnitPriceDTO> unitPriceDTO { get; set; } = new List<UnitPriceDTO>();
    }

    public class CreateProductHandler : IRequestHandler<CreateProduct, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitRepository _unitRepository;
        public CreateProductHandler(IProductRepository productRepository, IUnitRepository unitRepository)
        {
            _productRepository = productRepository;
            _unitRepository = unitRepository;
        }
        public async Task<CreateProductResponse> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
            // Get All units in the request to avoid awaiting in the foreach
            var unitIds = request.unitPriceDTO.Select(x => x.unitId).ToList();
            var Units = await _unitRepository.GetUnitsFromIds(unitIds);

            List<UnitPricePOCO> unitPrices = new List<UnitPricePOCO>();

            // Map UnitPriceDTO => UnitPricePOCO
            foreach (var entry in request.unitPriceDTO)
            {
                var unit = Units.FirstOrDefault(x => x.Id == entry.unitId);
                if (unit == null)
                    return new CreateProductResponse { IsSuccess = false, Id = 0 };
                unitPrices.Add(new UnitPricePOCO { unit = unit, Price = entry.unitPrice });
            }

            var result = Product.CreateProduct(request.name, unitPrices);

            if (result.IsFailed)
                return new CreateProductResponse { IsSuccess = false, Id = 0 };

            await _productRepository.CreateProduct(result.Value);
            await _productRepository.SaveChanges();
            return new CreateProductResponse { IsSuccess = true, Id = result.Value.Id };
        }
    }
}
