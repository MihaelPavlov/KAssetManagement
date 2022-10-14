namespace Location.API.Controllers
{
    using Location.Service.DTO;
    using Location.Service.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetLocationById(int locationId)
        {
            var result = await this.locationService.GetById(locationId);
            return this.Ok(result);
        }

        [HttpGet("get-all-locations/{organizationId}")]
        public async Task<IActionResult> GetLocationByOrganizationId(int organizationId)
        {
            // TODO: We should get all dependancies count of every location through microservice-to-microservice communication through GRPC
            var result = await this.locationService.GetAllByOrganizationId(organizationId);
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationRequest request)
        {
            var result = await this.locationService.Create(request);
            return this.Ok(result);
        }

        [HttpPut("{locationId}")]
        public async Task<IActionResult> UpdateLocation(int locationId, UpdateLocationRequest request)
        {
            //TODO: if (request.LocationId != locationId)
            //    throw new BadRequestException("Body and Route Identifier doesn't match!")

            await this.locationService.Update(request);
            return this.Ok();
        }

        [HttpDelete("{locationId}")]
        public async Task<IActionResult> DeleteLocation(int locationId)
        {
            //TODO: Check in the service layer: Is the location have dependancies.
            // We should check the dependancies through microservice-to-microservice communication through GRPC
            await this.locationService.Delete(locationId);
            return this.Ok();
        }
    }
}
