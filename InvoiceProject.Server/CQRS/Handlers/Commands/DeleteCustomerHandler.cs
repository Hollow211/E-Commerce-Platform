using Application.CQRS.Commands.Requests;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            // Validate the request
            if (request.id <= 0)
                return false;

            var customer = await _customerRepository.GetById(request.id);

            if (customer == null)
                return false;

            if (customer.CanDeleteCustomer(customer.id))
                return false;

            await _customerRepository.DeleteCustomer(customer);

            return true;
        }
    }
}
