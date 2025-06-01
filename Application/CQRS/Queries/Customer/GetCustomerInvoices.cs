using Domain.Aggregates.InvoiceAggregate;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Queries.CustomerQueries
{
    public record GetCustomerInvoicesResponse
    {
        public required bool isSuccess { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
    public record GetCustomerInvoices: IRequest<GetCustomerInvoicesResponse>
    {
        public required int id { get; set; }
    }

    public class GetCustomerInvoicesHandler : IRequestHandler<GetCustomerInvoices, GetCustomerInvoicesResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerInvoicesHandler(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<GetCustomerInvoicesResponse> Handle(GetCustomerInvoices request, CancellationToken cancellationToken)
        {
            var invoices = await _customerRepository.GetCustomerInvoices(request.id);

            if (invoices is null)
                return new GetCustomerInvoicesResponse { isSuccess = false, Invoices = null };
            else
                return new GetCustomerInvoicesResponse { isSuccess = true, Invoices = invoices };
        }
    }
}
