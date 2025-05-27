using Domain.Aggregates;

namespace Domain.POCO
{
    public record UnitPrice
    {
        public required Unit unit { get; init; }

        public required decimal Price { get; init; }
    }
}
