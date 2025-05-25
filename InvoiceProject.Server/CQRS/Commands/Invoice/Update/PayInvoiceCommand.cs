using MediatR;

namespace Application.CQRS.Commands.Invoice.Update
{
    public record PayInvoiceCommand: IRequest<bool>
    {
        public required int id { get; set; }
    }
}
