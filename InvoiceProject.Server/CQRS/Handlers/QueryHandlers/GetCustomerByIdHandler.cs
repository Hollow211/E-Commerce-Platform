using Application.CQRS.Queries.Requests;
using Application.CQRS.Queries.Responses;
using Domain.AggregateNodes;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Queries
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<GetCustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.id);

            if (customer is null)
                return new GetCustomerResponse { isSuccess = false, customer = null };
            else
                return new GetCustomerResponse { isSuccess = true, customer = customer };
        }
    }
}
