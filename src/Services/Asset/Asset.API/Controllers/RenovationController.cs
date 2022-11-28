namespace Asset.API.Controllers
{
    using Asset.Application.Queries.Renovation;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class RenovationController : ControllerBase
    {
        private readonly IMediator mediator;

        public RenovationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(GetRenovationRequestByIdQueryModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await this.mediator.Send(new GetRenovationRequestByIdQuery(id));

            return this.Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetRenovationRequestQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRequestList()
        {
            var result = await this.mediator.Send(new GetRenovationRequestListQuery());

            return this.Ok(result);
        }
    }
}
