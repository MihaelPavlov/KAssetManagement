namespace Asset.API.Controllers
{
    using Asset.Application.Commands;
    using Asset.Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IMediator mediator;
        public AssetController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(GetAssetByIdQueryModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAssetById(int id)
        {
            var result = await this.mediator.Send(new GetAssetByIdQuery(id));
            return this.Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAssetListQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAssetList()
        {
            var result = await this.mediator.Send(new GetAssetListQuery());
            return this.Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsset(CreateAssetCommand request)
        {
            var result = await this.mediator.Send(request);
            return this.Ok(result);
        }

        [HttpPut("id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsset(int id, UpdateAssetCommand request)
        {
            if (id != request.Id)
                throw new Exception("Body and route are not the same!");

            await this.mediator.Send(request);
            return this.Ok();
        }

        [HttpDelete("id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            await this.mediator.Send(new DeleteAssetCommand(id));
            return this.Ok();
        }
    }
}
