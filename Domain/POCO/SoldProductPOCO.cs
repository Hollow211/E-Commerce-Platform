using Domain.Aggregates;
using Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.POCO
{
    public record SoldProductPOCO
    {
        public Product Product { get; set; }

        public required Unit Unit { get; set; }

        public int Quantity { get; set; }
    }
}
