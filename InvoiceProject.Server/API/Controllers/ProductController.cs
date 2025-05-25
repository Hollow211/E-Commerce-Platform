using Application.CQRS.Commands.ProductCommands;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Application.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(CreateProduct request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
