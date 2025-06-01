using Domain.Aggregates;

namespace Domain.POCO
{
    public record UnitPricePOCO
    {
        public required Unit unit { get; init; }

        public required decimal Price { get; init; }
    }
}
