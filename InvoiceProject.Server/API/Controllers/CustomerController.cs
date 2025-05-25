using Application.CQRS.Commands.Requests;
using Application.CQRS.Queries.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Application.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpGet("overview/{id}")]
        public async Task<IActionResult> GetOverview(int id)
        {
            var result = await _mediator.Send(new GetCustomerInvoices { id = id });
            return Ok(result);
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetCustomerDetail(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { id = id });
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> GetCustomerDetail(CreateCustomerCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("updateName")]
        public async Task<IActionResult> GetCustomerDetail(UpdateCustomerNameCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
