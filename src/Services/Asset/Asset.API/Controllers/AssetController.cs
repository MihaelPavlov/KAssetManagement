namespace Asset.API.Controllers
{
    using Asset.Application.Persistence;
    using Asset.Application.Queries;
    using Asset.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository assetRepository;
        private readonly IMediator mediator;
        public AssetController(IAssetRepository assetRepository, IMediator mediator)
        {
            this.assetRepository = assetRepository;
            this.mediator = mediator;
        }

        [HttpGet("assetId")]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Asset>> GetAssetById(int assetId)
        {
            var result = await this.assetRepository.GetByIdAsync(assetId);
            return this.Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAssetListQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Asset>> GetAssetList()
        {
            var result = await this.mediator.Send(new GetAssetListQuery());
            return this.Ok(result);
        }
    }
}
