using Domain.Aggregates.InvoiceAggregate;
using Domain.Aggregates.ProductAggregate;
using Domain.Shared.Interfaces;
using MediatR;

namespace Application.CQRS.Commands.Invoice.Add
{
    public class AddInvoiceHandler: IRequestHandler<AddInvoiceCommand, AddInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public AddInvoiceHandler(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<AddInvoiceResponse> Handle(AddInvoiceCommand request, CancellationToken cancellationToken)
        {
            // Check if customer exists
            if (await _customerRepository.GetById(request.CustomerId) is null)
                return new AddInvoiceResponse { isSuccess = false };

            // get products
            var products = new Dictionary<Product,int>();
            foreach(var entry in request.Products)
            {
                var product = await _productRepository.GetById(entry.id);
                if (product is null)
                    return new AddInvoiceResponse { isSuccess = false };
                products.Add(product,entry.Quantity);
            }

            /*// Create invoice
            var result = Domain.Aggregates.InvoiceAggregate.Invoice.CreateInvoice(request.CustomerId, request.InvoiceDate,
                                                                                   request.TotalAmount, products);

            if (result.IsFailed)
                return new AddInvoiceResponse { isSuccess = false };

            // Add to db
            await _invoiceRepository.AddInvoiceAsync(result.Value);
            await _invoiceRepository.SaveChanges();
            */
            return new AddInvoiceResponse
            {
                isSuccess = true,
                id = 2 // Placeholder for the actual invoice ID after creation
            };
        }
    }
}
