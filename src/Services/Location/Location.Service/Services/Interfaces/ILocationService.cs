namespace Location.Service.Services.Interfaces
{
    using DAL = Location.Data.Entities;
    using Location.Service.DTO;

    public interface ILocationService
    {
        Task<Location> GetById(int id);
        Task<GetAllLocationsByOrganizationId> GetAllByOrganizationId(int organizationId);
        Task<int> Create(CreateLocationRequest request);
        Task Update(UpdateLocationRequest request);
        Task Delete(int id);
        Task CreateAssetLocation(DAL.AssetLocation request);
    }
}
