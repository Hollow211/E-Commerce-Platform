using Application.Interfaces;
using Domain.AggregateNodes;
using MediatR;

namespace Application.CQRS.Commands.CustomerCommands
{
    public record CreateCustomerResponse
    {
        public int id { get; set; }

        public bool isSuccess { get; set; }
    }
    public record CreateCustomer : IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }

    public class CreateCustomerHandler : IRequestHandler<CreateCustomer, CreateCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CreateCustomerResponse> Handle(CreateCustomer request, CancellationToken cancellationToken)
        {

            var result = Customer.CreateCustomer(request.Name, request.Email,
                request.Address, request.PhoneNumber);

            if (result.IsFailed)
                return new CreateCustomerResponse { isSuccess = false };

            await _customerRepository.AddCustomer(result.Value);
            await _customerRepository.SaveChanges();
            return new CreateCustomerResponse { isSuccess = true, id = result.Value.Id };
        }
    }
}
