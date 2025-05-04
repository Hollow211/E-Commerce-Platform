using Application.CQRS.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpGet("overview")]
        public async Task<IActionResult> Overview(int id)
        {
            var result = await _mediator.Send(new GetCustomerInvoices { id = id });
            return Ok(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
