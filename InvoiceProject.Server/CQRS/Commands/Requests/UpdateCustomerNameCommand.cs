using MediatR;

namespace Application.CQRS.Commands.Requests
{
    public record UpdateCustomerNameCommand : IRequest<bool>
    {
        public required int id { get; set; }

        public required string Name { get; set; }
    }
}
