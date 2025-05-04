using MediatR;

namespace Application.CQRS.Commands.Requests
{
    public record DeleteCustomerCommand: IRequest<bool>
    {
        public int id { get; set; }
    }
}
