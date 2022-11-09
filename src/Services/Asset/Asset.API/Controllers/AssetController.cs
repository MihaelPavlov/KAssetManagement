namespace Asset.API.Controllers
{
    using Asset.Application.Commands;
    using Asset.Application.Queries;
    using Asset.Domain.Entities;
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

        [HttpGet("assetId")]
        [ProducesResponseType(typeof(GetAssetByIdQueryModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAssetById(int assetId)
        {
            var result = await this.mediator.Send(new GetAssetByIdQuery(assetId));
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

        [HttpPut("assetId")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsset(int assetId, UpdateAssetCommand request)
        {
            if (assetId != request.AssetId)
                throw new Exception("Body and route are not the same!");

            await this.mediator.Send(request);
            return this.Ok();
        }

        [HttpDelete("assetId")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsset(int assetId)
        {
            await this.mediator.Send(new DeleteAssetCommand(assetId));
            return this.Ok();
        }
    }
}
