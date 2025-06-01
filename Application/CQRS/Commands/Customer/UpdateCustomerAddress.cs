using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Commands.CustomerCommands
{
    public record UpdateCustomerAddress : IRequest<bool>
    {
        public required int id { get; set; }

        public required string address { get; set; }
    }

    public class UpdateCustomerAdressHandler : IRequestHandler<UpdateCustomerAddress, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerAdressHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerAddress request, CancellationToken cancellationToken)
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
