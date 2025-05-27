using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Commands.CustomerCommands
{
    public record UpdateCustomerName : IRequest<bool>
    {
        public required int id { get; set; }

        public required string Name { get; set; }
    }

    public class UpdateCustomerNameHandler : IRequestHandler<UpdateCustomerName, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerNameHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerName request, CancellationToken cancellationToken)
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
