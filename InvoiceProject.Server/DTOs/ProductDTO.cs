namespace Application.DTOs
{
    public record ProductDTO
    {
        public required int id { get; set; }

        public required int Quantity { get; set; }

        public required decimal unitPrice { get; set; }

        public required bool isPackage { get; set; }

    }
}
