using Domain.AggregateNodes;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Queries.CustomerQueries
{
    public record GetCustomerResponse
    {
        public required bool isSuccess { get; set; }

        public Customer? customer { get; set; }
    }
    public record GetCustomer : IRequest<GetCustomerResponse>
    {
        public required int id { get; set; }
    }

    public class GetCustomerByIdHandler : IRequestHandler<GetCustomer, GetCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<GetCustomerResponse> Handle(GetCustomer request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.id);

            if (customer is null)
                return new GetCustomerResponse { isSuccess = false, customer = null };
            else
                return new GetCustomerResponse { isSuccess = true, customer = customer };
        }
    }
}
