using Application.CQRS.Queries.GetAllProduct;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.CQRS.Queries.UnitQueries;

namespace Application.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UnitController : Controller
    {
        private readonly IMediator _mediator;
        public UnitController(IMediator mediator) => _mediator = mediator;

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUnits()
        {
            var result = await _mediator.Send(new GetAllUnits());
            return Ok(result);
        }
    }
}
