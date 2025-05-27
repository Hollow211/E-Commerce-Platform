using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Commands.CustomerCommands
{
    public record DeleteCustomer: IRequest<bool>
    {
        public int id { get; set; }
    }

    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomer, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(DeleteCustomer request, CancellationToken cancellationToken)
        {
            // Validate the request
            if (request.id <= 0)
                return false;

            var customer = await _customerRepository.GetById(request.id);

            if (customer == null)
                return false;

            if (customer.CanDeleteCustomer(customer.Id))
                return false;

            await _customerRepository.DeleteCustomer(customer);

            return true;
        }
    }
}
