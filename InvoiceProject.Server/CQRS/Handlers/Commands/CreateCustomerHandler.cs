using Application.CQRS.Commands.Requests;
using Domain.AggregateNodes;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    public class CreateCustomerHandler: IRequestHandler<CreateCustomerRequest, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            // Validate the request
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Address) || string.IsNullOrEmpty(request.PhoneNumber))
            {
                return false;
            }
            var customer = new Customer
            {
                CustomerName = request.Name,
                CustomerEmail = request.Email,
                CustomerAddress = request.Address,
                CustomerPhoneNumber = request.PhoneNumber
            };
            await _customerRepository.AddCustomer(customer);
            return true;
        }
    }
}
