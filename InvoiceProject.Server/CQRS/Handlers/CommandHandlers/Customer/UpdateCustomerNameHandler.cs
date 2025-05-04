using Application.CQRS.Commands.Requests;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.CommandHandlers.Customer
{
    public class UpdateCustomerNameHandler : IRequestHandler<UpdateCustomerNameCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerNameHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerNameCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.id);

            if (customer == null)
                return false;

            customer.Name = request.Name;

            await _customerRepository.UpdateCustomer(customer);

            await _customerRepository.SaveChanges();

            return true;
        }
    }
}
