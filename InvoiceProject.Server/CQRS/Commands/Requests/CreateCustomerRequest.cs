using MediatR;

namespace Application.CQRS.Commands.Requests
{
    public record CreateCustomerRequest : IRequest<bool>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
