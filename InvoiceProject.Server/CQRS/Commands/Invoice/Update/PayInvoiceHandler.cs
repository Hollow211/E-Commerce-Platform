using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Commands.Invoice.Update
{
    public class PayInvoiceHandler : IRequestHandler<PayInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public PayInvoiceHandler(IInvoiceRepository invoiceRepository) => _invoiceRepository = invoiceRepository;
        public async Task<bool> Handle(PayInvoiceCommand request, CancellationToken cancellationToken)
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
