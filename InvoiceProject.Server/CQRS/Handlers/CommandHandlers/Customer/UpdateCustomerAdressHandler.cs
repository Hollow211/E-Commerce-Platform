using Application.CQRS.Commands.Requests;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.CommandHandlers
{
    public class UpdateCustomerAdressHandler : IRequestHandler<UpdateCustomerAdressCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerAdressHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerAdressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.id);

            if (customer == null)
                return false;

            customer.Address = request.address;

            await _customerRepository.UpdateCustomer(customer);

            await _customerRepository.SaveChanges();

            return true;
        }
    }
}
