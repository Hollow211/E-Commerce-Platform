using MediatR;

namespace Application.CQRS.Commands.Requests
{
    public record UpdateCustomerAdressCommand : IRequest<bool>
    {
        public required int id { get; set; }

        public required string address { get; set; }
    }
}
