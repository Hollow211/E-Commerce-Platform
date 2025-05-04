using Application.CQRS.Commands.Requests;
using Domain.Shared.Interfaces;
using Domain.AggregateNodes;
using MediatR;

namespace Application.CQRS.Handlers.CommandHandlers.CustomerCreation
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Domain.AggregateNodes.Customer
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber
            };
            await _customerRepository.AddCustomer(customer);
            await _customerRepository.SaveChanges();
            return true;
        }
    }
}
