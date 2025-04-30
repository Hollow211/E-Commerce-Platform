using MediatR;

namespace Application.CQRS.Commands.Requests
{
    public record DeleteCustomerRequest: IRequest<bool>
    {
        public int id { get; set; }
    }
}
