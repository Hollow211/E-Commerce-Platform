using Application.CQRS.Queries.GetAllProduct;
using Application.Interfaces;
using Domain.Aggregates.InvoiceAggregate;
using Domain.Aggregates.ProductAggregate;
using MediatR;

namespace Application.CQRS.Queries.InvoiceQueries
{
    public record GetInvoices : IRequest<List<Invoice>>
    {
        public required int customerId { get; set; }
    }

    public class GetInvoicesHandler : IRequestHandler<GetInvoices, List<Invoice>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public GetInvoicesHandler(IInvoiceRepository invoiceRepository) => _invoiceRepository = invoiceRepository;

        public async Task<List<Invoice>> Handle(GetInvoices request, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetInvoicesByCustomerId(request.customerId);
        }
    }
}
