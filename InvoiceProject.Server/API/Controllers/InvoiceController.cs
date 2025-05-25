using Application.CQRS.Commands.Invoice.Add;
using Application.CQRS.Commands.Invoice.Update;
using Application.CQRS.Commands.Requests;
using Application.CQRS.Queries.GetAllProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator) => _mediator = mediator;

        [HttpPost("add")]
        public async Task<IActionResult> AddInvoice(AddInvoiceCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpPost("pay")]
        public async Task<IActionResult> PayInvoice(PayInvoiceCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
