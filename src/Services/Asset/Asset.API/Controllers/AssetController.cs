namespace Asset.API.Controllers
{
    using Asset.Application.Persistence;
    using Asset.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository assetRepository;

        public AssetController(IAssetRepository assetRepository)
        {
            this.assetRepository = assetRepository;
        }

        [HttpGet("assetId")]
        [ProducesResponseType(typeof(Asset), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Asset>> GetAssetById(int assetId)
        {
            var result = await this.assetRepository.GetByIdAsync(assetId);
            return this.Ok(result);
        }
    }
}
