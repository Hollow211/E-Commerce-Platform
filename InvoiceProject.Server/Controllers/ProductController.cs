using Application.CQRS.Commands.ProductCommands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.CQRS.Queries.GetAllProduct;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProducts());
            return Ok(result);
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(CreateProduct request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
