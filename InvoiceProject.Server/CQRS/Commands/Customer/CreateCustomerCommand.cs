using Application.CQRS.Commands.Customer;
using MediatR;

namespace Application.CQRS.Commands.Requests
{
    public record CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
