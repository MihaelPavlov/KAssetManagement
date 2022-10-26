namespace Location.Data.Repositories.Interfaces
{
    using Location.Data.DTO;
    using Location.Data.Entities;

    public interface ILocationRepository
    {
        /// <summary>
        /// Returns location by unique identifier.
        /// </summary>
        /// <param name="locationId">A integer which representing the locaiton identifier.</param>
        /// <returns>A  <see cref="GetLocationById"/> model.</returns>
        Task<GetLocationById?> GetById(int locationId);

        /// <summary>
        /// Returns the count and collection with all location by organization.
        /// </summary>
        /// <param name="organizationId">A string representing the organization identifier.</param>
        /// <returns>A <see cref="GetAllByOrganizationId"/> model.</returns>
        Task<GetAllLocationsByOrganizationId> GetAllByOrganizationId(int organizationId);

        /// <summary>
        /// Create location.
        /// </summary>
        /// <param name="request">A model which representing the information about the location.</param>
        /// <returns>A integer representing the ID of the location.</returns>
        Task<int> CreateLocation(CreateLocation request);

        /// <summary>
        /// Update location.
        /// </summary>
        /// <param name="request">A model which representing the information about the location.</param>
        Task UpdateLocation(UpdateLocation request);

        /// <summary>
        /// Delete location.
        /// </summary>
        /// <param name="request">A model which representing the information about the location.</param>
        Task DeleteLocation(DeleteLocation request);

        Task CreateAssetLocation(AssetLocation request);
    }
}
