namespace Location.API.Controllers
{
    using Location.Data.DTO;
    using Location.Data.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository repository;

        public LocationController(ILocationRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetLocationById(int locationId)
        {
            var result = await this.repository.GetById(locationId);
            return this.Ok(result);
        }

        [HttpGet("get-all-locations/{organizationId}")]
        public async Task<IActionResult> GetLocationByOrganizationId(int organizationId)
        {
            // TODO: We should get all dependancies count of every location through microservice-to-microservice communication through GRPC
            var result = await this.repository.GetAllByOrganizationId(organizationId);
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocation request)
        {
            var result = await this.repository.CreateLocation(request);
            return this.Ok(result);
        }

        [HttpPut("{locationId}")]
        public async Task<IActionResult> UpdateLocation(int locationId, UpdateLocation request)
        {
            //TODO: if (request.LocationId != locationId)
            //    throw new BadRequestException("Body and Route Identifier doesn't match!")

            await this.repository.UpdateLocation(request);
            return this.Ok();
        }

        [HttpDelete("{locationId}")]
        public async Task<IActionResult> DeleteLocation(int locationId)
        {
            //TODO: Check in the service layer: Is the location have dependancies.
            // We should check the dependancies through microservice-to-microservice communication through GRPC
            await this.repository.DeleteLocation(new DeleteLocation(locationId));
            return this.Ok();
        }
    }
}
