
using Application.DTOs;
using Domain.Aggregates.InvoiceAggregate;
using Domain.POCO;
using Application.Interfaces;
using MediatR;
namespace Application.CQRS.Commands.InvoiceCommands
{
    public class CreateInvoiceResponse
    {
        public int id { get; set; }

        public bool isSuccess { get; set; }
    }
    public class CreateInvoice : IRequest<CreateInvoiceResponse>
    {
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<SoldProductDTO> Products { get; set; } = new List<SoldProductDTO>();
    }
    public class AddInvoiceHandler : IRequestHandler<CreateInvoice, CreateInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitRepository _unitRepository;

        public AddInvoiceHandler(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IUnitRepository unitRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitRepository = unitRepository;
        }

        public async Task<CreateInvoiceResponse> Handle(CreateInvoice request, CancellationToken cancellationToken)
        {
            // Get Customer
            var Customer = await _customerRepository.GetById(request.CustomerId);

            // Get list of products
            List<int> productIds = request.Products.Select(x => x.productId).ToList();
            var Products = await _productRepository.GetProductsFromIds(productIds);

            // Get list of units
            List<int> unitIds = request.Products.Select(x => x.unitId).ToList();
            var Units = await _unitRepository.GetUnitsFromIds(unitIds);

            // Map DTO to POCO
            // SoldProductDTO => SoldProductPOCO
            var SoldProducts = new List<SoldProductPOCO>();
            foreach (var entry in request.Products)
            {
                // Select Product
                var product = Products.FirstOrDefault(p => p.Id == entry.productId)!;

                // Select Unit
                var unit = Units.FirstOrDefault(u => u.Id == entry.unitId)!;

                // Add to Sold Products
                var newSoldProduct = new SoldProductPOCO { Product = product, Unit = unit, Quantity = entry.quantity };
                SoldProducts.Add(newSoldProduct);
            }

            //Create invoice
            var result = Invoice.CreateInvoice(Customer, 
                request.InvoiceDate,
                SoldProducts);

            if (result.IsFailed)
                return new CreateInvoiceResponse { isSuccess = false };

            // Add to db
            await _invoiceRepository.AddInvoiceAsync(result.Value);
            await _invoiceRepository.SaveChanges();

            return new CreateInvoiceResponse { id = result.Value.Id, isSuccess = true };
        }
    }
}
