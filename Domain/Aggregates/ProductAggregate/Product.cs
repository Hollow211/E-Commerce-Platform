using Domain.Aggregates.InvoiceAggregate;
using Domain.Aggregates.ProductUnitAggregate;
using Domain.POCO;
using Domain.Shared;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Domain.Aggregates.ProductAggregate
{
    public class Product : Entity<int>
    {
        public string Name { get; set; } = null!;

        public required virtual ICollection<ProductUnit> Units { get; set; }

        public static Result<Product> CreateProduct(string name,
                                                    List<UnitPrice> units)
        {
            // Validation
            if (!isValid(name, units))
                return Result.Fail("Invalid product data.");

            // Create product
            var product = new Product
            {
                Name = name,
                Units = new List<ProductUnit>()
            };

            // Create product units
            foreach (var entry in units)
            {
                if (entry.unit == null)
                    return Result.Fail("Unit cannot be null.");
                if (entry.Price <= 0)
                    return Result.Fail("Price must be greater than zero.");
                // Create product unit
                var productUnit = ProductUnit.CreateProductUnit(product.Id, entry.unit.Id, entry.Price);

                if (productUnit.IsFailed)
                    return Result.Fail(productUnit.Errors);

                // Passed everything, add to product units
                product.Units.Add(productUnit.Value);
            }

            return Result.Ok(product);
        }

        public void FilterUnits(List<int> unitIds)
        {
            Units = Units.Where(u => unitIds.Any(unit => unit == u.UnitId)).ToList();
        }

        private static bool isValid(string name, List<UnitPrice> units)
        {
            // Add validation logic here
            if (string.IsNullOrWhiteSpace(name))
                return false;
            if (units == null || units.Count == 0)
                return false;
            return true;
        }
    }
}


