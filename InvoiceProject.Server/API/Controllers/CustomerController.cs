using Application.CQRS.Queries.Requests;
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
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
