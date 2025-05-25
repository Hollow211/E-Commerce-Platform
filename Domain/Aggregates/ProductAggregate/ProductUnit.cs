using Domain.Shared;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ProductUnitAggregate
{
    public class ProductUnit: Entity<int>
    {
        public required int ProductId { get; set; }

        public required int UnitId { get; set; }

        public required decimal UnitPrice { get; set; }

        public static Result<ProductUnit> CreateProductUnit(int productId, int unitId, decimal unitPrice)
        {
            // Create product unit
            var productUnit = new ProductUnit
            {
                ProductId = productId,
                UnitId = unitId,
                UnitPrice = unitPrice
            };
            return Result.Ok(productUnit);
        }
    }
}
