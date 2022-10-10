namespace Location.API.Controllers
{
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
            var result = await this.repository.GetAllByOrganizationId(organizationId);
            return this.Ok(result);
        }
    }
}
