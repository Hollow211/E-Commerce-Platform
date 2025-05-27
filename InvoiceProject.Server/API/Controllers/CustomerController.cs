using Application.CQRS.Commands.CustomerCommands;
using Application.CQRS.Queries.CustomerQueries;
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

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetCustomerDetail(int id)
        {
            var result = await _mediator.Send(new GetCustomer { id = id });
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomer request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("updateName")]
        public async Task<IActionResult> UpdateCustomerName(UpdateCustomerName request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
