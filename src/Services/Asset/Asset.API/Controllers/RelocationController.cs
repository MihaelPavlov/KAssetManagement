namespace Asset.API.Controllers
{
    using Asset.Application.Commands;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class RelocationController : ControllerBase
    {
        private readonly IMediator mediator;

        public RelocationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateRequest(CreateRelocationRequestCommand request)
        {
            var result = await mediator.Send(request);

            return this.Ok(result);
        }
    }
}
