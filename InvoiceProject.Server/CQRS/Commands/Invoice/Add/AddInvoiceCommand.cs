using Application.CQRS.Commands.Customer;
using Application.DTOs;
using Domain.AggregateNodes;
using MediatR;

namespace Application.CQRS.Commands.Invoice.Add
{
    public class AddInvoiceCommand : IRequest<AddInvoiceResponse>
    {
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
