namespace Application.CQRS.Commands.Customer
{
    public record CreateCustomerResponse
    {
        public int id { get; set; }

        public bool isSuccess { get; set; }
    }
}
