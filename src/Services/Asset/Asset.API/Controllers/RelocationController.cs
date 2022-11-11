namespace Asset.API.Controllers
{
    using Asset.Application.Commands.Relocation;
    using Asset.Application.Queries.Relocation;
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

        [HttpGet("id")]
        [ProducesResponseType(typeof(GetRelocationRequestByIdQueryModel),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await this.mediator.Send(new GetRelocationRequestByIdQuery(id));

            return this.Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetRelocationRequestByIdQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRequestList()
        {
            var result = await this.mediator.Send(new GetRelocationRequestListQuery());

            return this.Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateRequest(CreateRelocationRequestCommand request)
        {
            var result = await mediator.Send(request);

            return this.Ok(result);
        }

        [HttpPut("id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRequestStatus(int id, UpdateRelocationRequestStatusCommand request)
        {
            if (id != request.Id)
                throw new Exception("Body and route are not the same!");

            await this.mediator.Send(request);
            return this.Ok();
        }

        [HttpDelete("id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRequestStatus(int id)
        {
            await this.mediator.Send(new DeleteRelocationRequestCommand(id));
            return this.Ok();
        }
    }
}
