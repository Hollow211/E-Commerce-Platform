using Application.CQRS.Commands.Requests;
using Domain.Shared.Interfaces;
using Domain.AggregateNodes;
using MediatR;
using Application.CQRS.Commands.Customer;

namespace Application.CQRS.Handlers.CommandHandlers.CustomerCreation
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            var result = Customer.CreateCustomer(request.Name, request.Email,
                request.Address, request.PhoneNumber);

            if (result.IsFailed)
                return new CreateCustomerResponse { isSuccess = false};

            await _customerRepository.AddCustomer(result.Value);
            await _customerRepository.SaveChanges();
            return new CreateCustomerResponse { isSuccess = true, id = result.Value.Id};
        }
    }
}
