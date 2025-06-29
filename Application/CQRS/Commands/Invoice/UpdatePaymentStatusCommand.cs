using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Commands.InvoiceCommands
{
    public record UpdatePaymentStatusCommand: IRequest<bool>
    {
        public required int id { get; set; }
    }

    public class PayInvoiceHandler : IRequestHandler<UpdatePaymentStatusCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public PayInvoiceHandler(IInvoiceRepository invoiceRepository) => _invoiceRepository = invoiceRepository;
        public async Task<bool> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            // Get Invoice
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(request.id);

            if (invoice is null)
                return false;

            invoice.isPaid = true;

            await _invoiceRepository.UpdateInvoiceAsync(invoice);
            await _invoiceRepository.SaveChanges();

            return true;
        }
    }
}
