using Application.CQRS.Queries.Customer;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.QueryHandlers.Customer
{
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
