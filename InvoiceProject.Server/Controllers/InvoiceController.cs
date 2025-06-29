using Application.CQRS.Commands.InvoiceCommands;
using Application.CQRS.Queries.InvoiceQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator) => _mediator = mediator;

        [HttpGet("overview/{id}")]
        public async Task<IActionResult> GetOverview(int id)
        {
            var result = await _mediator.Send(new GetInvoices { customerId = id });
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("pay")]
        public async Task<IActionResult> PayInvoice(UpdatePaymentStatusCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
