using Domain.Aggregates;

namespace Domain.ValueObjects
{
    public record UnitPrice
    {
        public required Unit unit { get; init; }

        public required decimal Price { get; init; }
    }
}
