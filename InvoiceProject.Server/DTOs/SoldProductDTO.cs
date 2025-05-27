namespace Application.DTOs
{
    public record SoldProductDTO
    {
        public required int productId { get; set; }

        public required int unitId { get; set; }

        public required int quantity { get; set; }
    }
}
